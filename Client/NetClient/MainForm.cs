using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KeyAuthorization;
using FileConfig;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Policy;

namespace NetClient
{
    public partial class MainForm : XtraForm
    {
        /// <summary>
        /// Keep running background time check?
        /// </summary>
        private bool timeUpdates = false;

        /// <summary>
        /// C:\Users\{USER}\AppData\Local\cheatconfig\userconfig.ini
        /// </summary>
        private ProjectConfigFile ConfigFile;

        /// <summary>
        /// Cheat Authentication Api
        /// </summary>
        private ClientAuth ClientAuthenticator;

        /// <summary>
        /// Our current login state.
        /// </summary>
        private ClientAuth.LoginState LoginState = ClientAuth.LoginState.Not_logged_In;

        /// <summary>
        /// Games we have cheats for.
        /// </summary>
        private List<ClientAuth.CheatItems> cheats;

        /// <summary>
        /// AppDomain for cheat forms
        /// </summary>
        private AppDomain appDomain = AppDomain.CreateDomain("cheatsDomain");

        /// <summary>
        /// The current cheat form opened up.
        /// </summary>
        private XtraForm cheatForm = new XtraForm();

        /// <summary>
        /// Buttons for games.
        /// </summary>
        private List<TileItem> TileItems;

        public MainForm()
        {
            if (Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\dnSpy\\") || Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\dnSpy\\"))
            {
                Process.GetCurrentProcess().Kill();
            }

            InitializeComponent();

            TileItems = new List<TileItem>();
            ConfigFile = new ProjectConfigFile("cheatconfig", "userconfig", new string[] { "auth", "user", "pass" });

            if (ConfigFile["auth"].Equals("1"))
            {
                UsernameTextbox.Text = ConfigFile["user"];
                PasswordTextbox.Text = ConfigFile["pass"];
            }

            else
            {
                MainTab.SelectedTabPage = RegisterTab;
            }
        }

        /// <summary>
        /// Show the client an error if we fail auth.
        /// </summary>
        /// <returns></returns>
        private Task checkAuthentication()
        {
            while (ClientAuthenticator.Authorized) ;

            this.Invoke(new MethodInvoker(delegate
            {
                TimeTab.PageEnabled = false;
                RedeemKeyTab.PageEnabled = false;
                GameCheatTab.PageEnabled = false;
                RegisterTab.PageEnabled = true;
                LoginButton.Enabled = true;
            }));

            XtraMessageBox.Show("Authentication to server failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Process.GetCurrentProcess().Kill();
            return Task.CompletedTask;
        }

        /// <summary>
        /// If the client is not authorized with time left, disable the Time Tab and Game Tab
        /// </summary>
        /// <returns></returns>
        private Task checkAuthTime()
        {
            while (ClientAuthenticator.Authorized)
            {
                if (ClientAuthenticator.Authorized && !ClientAuthenticator.AuthorizedWithTimeLeft)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        MainTab.SelectedTabPage = LoginTab;
                        TimeTab.PageEnabled = false;
                        GameCheatTab.PageEnabled = false;
                    }));

                    XtraMessageBox.Show("Your out of time!", "Notice!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            ClientAuthenticator = new ClientAuth();

            if (ClientAuthenticator.RegisterUser(RegisterEmailTextbox.Text, RegisterUsernameTextbox.Text, RegisterPasswordTextbox.Text))
            {
                ConfigFile["auth"] = "1";
                ConfigFile["user"] = RegisterUsernameTextbox.Text;
                ConfigFile["pass"] = RegisterPasswordTextbox.Text;

                MainTab.SelectedTabPage = RedeemKeyTab;

                XtraMessageBox.Show("User Registered!", "Register User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                XtraMessageBox.Show("User Registy failed!", "Register User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, EventArgs e)
        {
            ClientAuthenticator = new ClientAuth();
            LoginState = ClientAuthenticator.Login(UsernameTextbox.Text, PasswordTextbox.Text, "x64");

            if (LoginState.Equals(ClientAuth.LoginState.Logged_In))
            {
                ConfigFile["auth"] = "1";
                ConfigFile["user"] = UsernameTextbox.Text;
                ConfigFile["pass"] = PasswordTextbox.Text;

                TimeTab.PageEnabled = true;
                RedeemKeyTab.PageEnabled = true;
                GameCheatTab.PageEnabled = true;
                LoginButton.Enabled = false;
                RegisterTab.PageEnabled = false;
                MainTab.SelectedTabPage = GameCheatTab;

                Task.Run(() => checkAuthentication()); // General Auth Check.
                Task.Run(() => checkAuthTime()); // Check that the user is authorized with time left.

                LoadCheats();

                XtraMessageBox.Show($"Logged in!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if(LoginState.Equals(ClientAuth.LoginState.Logged_In_Without_Time))
            {
                ConfigFile["auth"] = "1";
                ConfigFile["user"] = UsernameTextbox.Text;
                ConfigFile["pass"] = PasswordTextbox.Text;

                RedeemKeyTab.PageEnabled = true;
                RegisterTab.PageEnabled = false;
                LoginButton.Enabled = false;
                MainTab.SelectedTabPage = RedeemKeyTab;

                Task.Run(() => checkAuthentication());

                XtraMessageBox.Show($"Logged in!\nNotice: You're out of time!", "Notice!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.Password_Failure))
            {
                XtraMessageBox.Show("Password Mismatch failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.IP_Mismatch))
            {
                XtraMessageBox.Show("User IP Address Mismatch failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.User_doesnt_Exist))
            {
                XtraMessageBox.Show("User doesnt exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.Response_Error))
            {
                XtraMessageBox.Show("Server Response failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.User_Banned))
            {
                XtraMessageBox.Show("We don't like you, go away.", "Banned", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Redeem a key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedeemKeyButton_Click(object sender, EventArgs e)
        {
            if (ClientAuthenticator.Authorized)
            {
                if(ClientAuthenticator.RedeemKey($"{RedeemKeyTextbox1.Text}-{RedeemKeyTextbox2.Text}-{RedeemKeyTextbox3.Text}-{RedeemKeyTextbox4.Text}"))
                {
                    LoginState = ClientAuth.LoginState.Logged_In;
                    Task.Run(() => checkAuthTime());

                    TimeTab.PageEnabled = true;
                    RedeemKeyTab.PageEnabled = true;
                    GameCheatTab.PageEnabled = true;
                    LoginButton.Enabled = false;
                    MainTab.SelectedTabPage = GameCheatTab;

                    XtraMessageBox.Show("Key Redeemed Sucessfully!", "Redeem Key", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    XtraMessageBox.Show("Failed to Redeem key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                XtraMessageBox.Show("User isn't authorized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates the times on other threads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTab_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
           if(e.Page.Name.Equals("TimeTab"))
           {
                timeUpdates = true;
                Task.Run(() => updateTimesEveryHalfMinute());
           }

           else
           {
                if(!TimeCounterLabel.Text.Equals("Loading Times..."))
                {
                    TimeCounterLabel.Text = "Loading Times...";
                    TimeCounterLabel.Location = new System.Drawing.Point((TimeTab.Width / 2) - (TimeCounterLabel.Width / 2), TimeCounterLabel.Location.Y);
                }
                
                timeUpdates = false;
           }
        }

        /// <summary>
        /// Update the time every minute.
        /// </summary>
        /// <returns></returns>
        private Task updateTimesEveryHalfMinute()
        {
            while (ClientAuthenticator.Authorized && timeUpdates)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    EndDateLabel.Text = "End Date Time: " + ClientAuthenticator.TimeLeft.ToLocalTime().ToString();

                    int iYears = ClientAuthenticator.YearsLeft;
                    int iMonths = ClientAuthenticator.MonthsLeft;
                    int iDays = ClientAuthenticator.DaysLeft;
                    int iHours = ClientAuthenticator.HoursLeft;
                    int iMinutes = ClientAuthenticator.MinutesLeft;

                    string Years = !iYears.Equals(0) ? ($"{ClientAuthenticator.YearsLeft} " + (iYears.Equals(1) ? "Year, " : "Years, ")) : string.Empty;
                    string Months = !iMonths.Equals(0) ? ($"{ClientAuthenticator.MonthsLeft} " + (iMonths.Equals(1) ? "Month, " : "Months, ")) : string.Empty;
                    string Days = !iDays.Equals(0) ? ($"{ClientAuthenticator.DaysLeft} " + (iDays.Equals(1) ? "Day, " : "Days, ")) : string.Empty;
                    string Hours = !iHours.Equals(0) ? ($"{ClientAuthenticator.HoursLeft} " + (iHours.Equals(1) ? "Hour, " : "Hours, ")) : string.Empty;
                    string Minutes = !iMinutes.Equals(0) ? ($"{ClientAuthenticator.MinutesLeft} " + (iMinutes.Equals(1) ? "Minute" : "Minutes")) : string.Empty;
                    TimeCounterLabel.Text = Years + Months + Days + Hours + Minutes + " left";

                    TimeCounterLabel.Location = new System.Drawing.Point((TimeTab.Width / 2) - (TimeCounterLabel.Width / 2), TimeCounterLabel.Location.Y);
                }));

                Thread.Sleep(30000);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Loads the cheats to the tile controll
        /// </summary>
        private void LoadCheats()
        {
            if (ClientAuthenticator.GameCheats != null)
            {
                cheats = ClientAuthenticator.GameCheats;
                TileItems.Clear();

                for (int iItem = 0; iItem < cheats.Count; iItem++)
                {
                    TileItem item = new TileItem();
                    TileItemElement element = new TileItemElement();
                    element.Text = cheats[iItem].cheatname;


                    item.Elements.Add(element);
                    item.Id = iItem;
                    item.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
                    item.Name = cheats[iItem].shortname;

                    item.AllowAnimation = true;
                    item.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    item.ItemClick += TileHandler;
                    TileItems.Add(item);

                    CheatGroup.Items.Add(TileItems[iItem]);
                }
            }
        }

        /// <summary>
        /// Get the cheat Form of the game that we have cheats for.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [LoaderOptimizationAttribute(LoaderOptimization.MultiDomain)]
        private void TileHandler(object sender, TileItemEventArgs e)
        {
            TileItem item = (TileItem)sender;
            ClientAuth.ToolConfig cheatFiles = ClientAuthenticator.DownloadCheat($"{ResolveName(item.Text)}");

            try
            {
                if(cheatForm != null)
                {
                    // Attempt to trigger the garbage collector do its job.
                    cheatForm.Close();
                    cheatForm = null; 
                }

                AppDomain.Unload(appDomain);

                appDomain = AppDomain.CreateDomain("cheatsDomain");

                AppDomainBridge isolationDomainLoadContext = (AppDomainBridge)appDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof (AppDomainBridge).ToString());

                // Form is MarshalByRefObject type for the current AppDomain
                cheatForm = isolationDomainLoadContext.ExecuteFromAssembly(cheatFiles.dll, $"{ResolveClass(item.Text)}") as XtraForm;

                cheatForm.Show();
            }

            catch (Exception Ex)
            {
                XtraMessageBox.Show("Failed to load cheat!", e.Item.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Acts as a shared domain 
        /// </summary>
        private class AppDomainBridge : MarshalByRefObject
        {
            public Object ExecuteFromAssembly(Byte[] rawAsm, string typeName)
            {
                Assembly assembly = AppDomain.CurrentDomain.Load(rawAssembly: rawAsm);

                return Activator.CreateInstance(assembly.GetType(typeName));
            }
        }

        /// <summary>
        /// Get the game cheat uri name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string ResolveName(string name)
        {
            for (int iCheat = 0;iCheat < cheats.Count;iCheat++)
            {
                if (name == cheats[iCheat].cheatname)
                {
                    return cheats[iCheat].shortname;
                }
            }
            return string.Empty;
        }


        /// <summary>
        /// Get the game cheat class name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string ResolveClass(string name)
        {
            for (int iCheat = 0; iCheat < cheats.Count; iCheat++)
            {
                if (name == cheats[iCheat].cheatname)
                {
                    return cheats[iCheat].classname;
                }
            }
            return string.Empty;
        }
    }
}
