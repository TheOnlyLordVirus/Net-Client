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

            MessageBox.Show("Authentication to server failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    MessageBox.Show("Your out of time!", "Notice!", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                MessageBox.Show("User Registered!", "Register User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("User Registy failed!", "Register User", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            LoginState = ClientAuthenticator.Login(UsernameTextbox.Text, PasswordTextbox.Text);

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

                MessageBox.Show($"Logged in!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                MessageBox.Show($"Logged in!\nNotice: You're out of time!", "Notice!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.Password_Failure))
            {
                MessageBox.Show("Password Mismatch failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.IP_Mismatch))
            {
                MessageBox.Show("User IP Address Mismatch failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.User_doesnt_Exist))
            {
                MessageBox.Show("User doesnt exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.Response_Error))
            {
                MessageBox.Show("Server Response failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (LoginState.Equals(ClientAuth.LoginState.User_Banned))
            {
                MessageBox.Show("We don't like you, go away.", "Banned", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    MessageBox.Show("Key Redeemed Sucessfully!", "Redeem Key", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show("Failed to Redeem key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                MessageBox.Show("User isn't authorized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates the times on other threads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTab_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
           if(e.Page.Name == "TimeTab")
           {
                timeUpdates = true;
                Task.Run(() => updateTimesEveryHalfMinute());
           }

           else
           {
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
                    YearLabel.Text = "Years: " + ClientAuthenticator.YearsLeft.ToString();
                    MonthLabel.Text = "Months: " + ClientAuthenticator.MonthsLeft.ToString();
                    DayLabel.Text = "Days: " + ClientAuthenticator.DaysLeft.ToString();
                    HourLabel.Text = "Hours: " + ClientAuthenticator.HoursLeft.ToString();
                    MinuteLabel.Text = "Minutes: " + ClientAuthenticator.MinutesLeft.ToString();
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


        /// <summary>
        /// Get the cheat Form of the game that we have cheats for.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileHandler(object sender, TileItemEventArgs e)
        {
            TileItem item = (TileItem)sender;
            byte[] array = ClientAuthenticator.DownloadCheat($"{ResolveName(item.Text)}");

            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(array);
                Type type = assembly.GetType($"{e.Item.Name}.MainForm");
                object obj = Activator.CreateInstance(type);
                XtraForm form = obj as XtraForm;
                form.Show();
            }

            catch (Exception Ex)
            {
                MessageBox.Show("Failed to load cheat!", e.Item.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Get the game cheat name.
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
    }
}
