﻿using System;
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


        private List<ClientAuth.CheatItems> cheats;
        private List<TileItem> TileItems;
        public MainForm()
        {
            if (Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\dnSpy\\") || Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\dnSpy\\"))
            {
                Process.GetCurrentProcess().Kill();
            }
            InitializeComponent();
            ClientAuthenticator = new ClientAuth();
            ConfigFile = new ProjectConfigFile("cheatconfig", "userconfig", new string[] { "auth", "user", "pass" });

            if (ConfigFile["auth"].Equals("1"))
            {
                UsernameTextbox.Text = ConfigFile["user"];
                PasswordTextbox.Text = ConfigFile["pass"];
            }
            TileItems = new List<TileItem>();
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
            }));

            MessageBox.Show("Error", "Authentication to server failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return Task.CompletedTask;
        }


        /// <summary>
        /// If the client is not authorized with time left, disable the Time Tab and Game Tab
        /// </summary>
        /// <returns></returns>
        private Task checkAuthTime()
        {
            while(ClientAuthenticator.Authorized)
            {
                if(!ClientAuthenticator.AuthorizedWithTimeLeft)
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
        /// Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, EventArgs e)
        {
            LoginState = ClientAuthenticator.Login(UsernameTextbox.Text, PasswordTextbox.Text);

            if (LoginState.Equals(ClientAuth.LoginState.Logged_In))
            {
                ConfigFile["auth"] = "1";
                ConfigFile["user"] = UsernameTextbox.Text;
                ConfigFile["pass"] = PasswordTextbox.Text;

                TimeTab.PageEnabled = true;
                RedeemKeyTab.PageEnabled = true;
                GameCheatTab.PageEnabled = true;

                Task.Run(() => checkAuthentication());

                MessageBox.Show($"Logged in!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCheats();
            }

            else if(LoginState.Equals(ClientAuth.LoginState.Logged_In_Without_Time))
            {
                ConfigFile["auth"] = "1";
                ConfigFile["user"] = UsernameTextbox.Text;
                ConfigFile["pass"] = PasswordTextbox.Text;

                RedeemKeyTab.PageEnabled = true;
                Task.Run(() => checkAuthentication());

                MessageBox.Show($"Logged in!\nYour out of time!", "Notice!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        /// <summary>
        /// Redeem a key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedeemKeyButton_Click(object sender, EventArgs e)
        {
            if (ClientAuthenticator.Authorized && ClientAuthenticator.RedeemKey($"{RedeemKeyTextbox1.Text}-{RedeemKeyTextbox2.Text}-{RedeemKeyTextbox3.Text}-{RedeemKeyTextbox4.Text}"))
            {
                if(LoginState.Equals(ClientAuth.LoginState.Logged_In_Without_Time))
                {
                    LoginState = ClientAuth.LoginState.Logged_In;
                    Task.Run(() => checkAuthTime());
                }
                MessageBox.Show("Key Redeemed Sucessfully!", "Redeem Key", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Register a new user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if(ClientAuthenticator.RegisterUser(RegisterEmailTextbox.Text, RegisterUsernameTextbox.Text, RegisterPasswordTextbox.Text))
            {
                MessageBox.Show("User Registered!", "Register User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("User Registy failed!", "Register User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Loads the cheats from the x64 or x86 json.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ReloadCheats_Click(object sender, EventArgs e)
        {
            LoadCheats();
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

            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(array);
            Type type = assembly.GetType($"{e.Item.Name}.MainForm");
            object obj = Activator.CreateInstance(type);
            XtraForm form = obj as XtraForm;
            form.Show();
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
