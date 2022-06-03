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

        /// <summary>
        /// Have we loaded the cheats from the server yet?
        /// </summary>
        private bool cheatsLoaded = false;

        /// <summary>
        /// Version of loader
        /// </summary>
        private string version;

        public MainForm()
        {
            File.SetAttributes($"{AppDomain.CurrentDomain.BaseDirectory}\\Newtonsoft.Json.dll", FileAttributes.Hidden);
            File.SetAttributes($"{AppDomain.CurrentDomain.BaseDirectory}\\Net Client.exe.config", FileAttributes.Hidden);

            foreach (string file in Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}"))
            {
                if ((file.Contains("DevExpress") || file.Contains("Updater") && !file.Contains(".exe")))
                {
                    File.SetAttributes($"{file}", FileAttributes.Hidden);
                }
            }

            version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\dnSpy\\") || Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\dnSpy\\"))
            {
                this.Close();
            }

            ClientAuthenticator = new ClientAuth();

            InitializeComponent();

            this.Text = $"Net Client v{version}";

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
            while (ClientAuthenticator.Authorized) Thread.Sleep(1000);

            this.Invoke(new MethodInvoker(delegate
            {
                TimeTab.PageEnabled = false;
                RedeemKeyTab.PageEnabled = false;
                GameCheatTab.PageEnabled = false;
                RegisterTab.PageEnabled = true;
                LoginButton.Enabled = true;
            }));


            Task.Delay(5000).GetAwaiter().OnCompleted (() => { this.Close(); });

            XtraMessageBox.Show("Authentication to server failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
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
                        cheatForm.Close();
                    }));

                    XtraMessageBox.Show("Your out of time!", "Notice!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }

                Thread.Sleep(1000);
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
            if (ClientAuthenticator.RegisterUser(RegisterEmailTextbox.Text, RegisterUsernameTextbox.Text, RegisterPasswordTextbox.Text))
            {
                ConfigFile["auth"] = "1";
                ConfigFile["user"] = RegisterUsernameTextbox.Text;
                ConfigFile["pass"] = RegisterPasswordTextbox.Text;

                UsernameTextbox.Text = ConfigFile["user"];
                PasswordTextbox.Text = ConfigFile["pass"];

                MainTab.SelectedTabPage = LoginTab;

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
            LoginState = ClientAuthenticator.Login(UsernameTextbox.Text, PasswordTextbox.Text, "external", "x64");

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

                if (!cheatsLoaded)
                {
                    cheatsLoaded = true;
                    LoadCheats();
                }

                XtraMessageBox.Show($"Logged in!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.Logged_In_Without_Time))
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

            else if (LoginState.Equals(ClientAuth.LoginState.User_doesnt_Exist))
            {
                XtraMessageBox.Show("User doesnt exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.IP_Mismatch))
            {
                Task.Delay(5000).GetAwaiter().OnCompleted(() => { this.Close(); });

                XtraMessageBox.Show("User IP Address Mismatch failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            else if (LoginState.Equals(ClientAuth.LoginState.Response_Error))
            {
                Task.Delay(5000).GetAwaiter().OnCompleted(() => { this.Close(); });

                XtraMessageBox.Show("Server Response failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            else if (LoginState.Equals(ClientAuth.LoginState.User_Banned))
            {
                Task.Delay(5000).GetAwaiter().OnCompleted(() => { this.Close(); });

                XtraMessageBox.Show("We don't like you, go away.", "Banned", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
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
                bool KeyServerFlag = false;

                #pragma warning disable CS8629 // Nullable value type may be null.
                if ((bool)hyphenCheck.Checked)
                {
                    KeyServerFlag = ClientAuthenticator.RedeemKey($"{RedeemKeyTextbox1.Text}-{RedeemKeyTextbox2.Text}-{RedeemKeyTextbox3.Text}-{RedeemKeyTextbox4.Text}");
                }

                else
                {
                    KeyServerFlag = ClientAuthenticator.RedeemKey($"{RedeemKeyTextbox.Text}");
                }
                #pragma warning restore CS8629 // Nullable value type may be null.

                if (KeyServerFlag)
                {
                    LoginState = ClientAuth.LoginState.Logged_In;
                    Task.Run(() => checkAuthTime());

                    TimeTab.PageEnabled = true;
                    RedeemKeyTab.PageEnabled = true;
                    GameCheatTab.PageEnabled = true;
                    LoginButton.Enabled = false;
                    MainTab.SelectedTabPage = GameCheatTab;

                    if(!cheatsLoaded)
                    {
                        cheatsLoaded = true;
                        LoadCheats();
                    }

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
                    EndDateLabel.Text = "Expiration Date: " + ClientAuthenticator.TimeLeft.ToString();

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
                    TimeCounterLabel.Text = Years + Months + Days + Hours + Minutes;

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
        private void TileHandler(object sender, TileItemEventArgs e)
        {
            TileItem item = (TileItem)sender;
            byte[] cheatFile = ClientAuthenticator.DownloadCheat("external", $"{ResolveName(item.Text)}");

            try
            {
                if(cheatForm != null)
                {
                    cheatForm.Close();
                }
                Assembly asm = Assembly.Load(cheatFile);
                cheatForm = Activator.CreateInstance(asm.GetType($"{ResolveClass(item.Text)}")) as XtraForm;
                cheatForm.Show();
            }

            catch (Exception Ex)
            {
                XtraMessageBox.Show("Failed to load cheat!", e.Item.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /// <summary>
        /// Register user when enter is hit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterPasswordTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Keys.Enter))
            {
                if (RegisterButton.Enabled)
                {
                    RegisterButton.PerformClick();
                }
            }
        }

        /// <summary>
        /// Login when user presses enter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                if(LoginButton.Enabled)
                {
                    LoginButton.PerformClick();
                }
            }
        }


        /// <summary>
        /// Show message boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hyphenCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(hyphenCheck.Checked)
            {
                RedeemKeyTextbox.Visible = false;
                RedeemKeyTextbox.Enabled = false;

                RedeemKeyTextbox1.Visible = true;
                RedeemKeyTextbox1.Enabled = true;

                RedeemKeyTextbox2.Visible = true;
                RedeemKeyTextbox1.Visible = true;

                RedeemKeyTextbox3.Visible = true;
                RedeemKeyTextbox1.Visible = true;

                RedeemKeyTextbox4.Visible = true;
                RedeemKeyTextbox1.Visible = true;

                hyphen1.Visible = true;
                hyphen2.Visible = true;
                hyphen3.Visible = true;
            }

            else
            {
                RedeemKeyTextbox.Visible = true;
                RedeemKeyTextbox.Enabled = true;

                RedeemKeyTextbox1.Visible = false;
                RedeemKeyTextbox1.Enabled = false;

                RedeemKeyTextbox2.Visible = false;
                RedeemKeyTextbox1.Visible = false;

                RedeemKeyTextbox3.Visible = false;
                RedeemKeyTextbox1.Visible = false;

                RedeemKeyTextbox4.Visible = false;
                RedeemKeyTextbox1.Visible = false;

                hyphen1.Visible = false;
                hyphen2.Visible = false;
                hyphen3.Visible = false;
            }
        }
    }
}
