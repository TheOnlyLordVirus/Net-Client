using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KeyAuthorization;
using FileConfig;

namespace NetClient
{
    public partial class MainForm : XtraForm
    {
        private bool timeUpdates = false;
        private ProjectConfig ConfigFile;
        private ClientAuth ClientAuthenticator;
        private ClientAuth.LoginState LoginState;

        public MainForm()
        {
            InitializeComponent();
            ClientAuthenticator = new ClientAuth();
            ConfigFile = new ProjectConfig("cheatconfig", "userconfig", new string[] { "auth", "user", "pass" });

            if (ConfigFile["auth"].Equals("1"))
            {
                UsernameTextbox.Text = ConfigFile["user"];
                PasswordTextbox.Text = ConfigFile["pass"];
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
        private Task updateTimesEveryMinute()
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

                Thread.Sleep(60000);
            }
            
            return Task.CompletedTask;
        }

        /// <summary>
        /// Update the time every second.
        /// </summary>
        /// <returns></returns>
        private Task updateSeconds()
        {
            while (ClientAuthenticator.Authorized && timeUpdates)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    SecondsLabel.Text = "Seconds: " + ClientAuthenticator.SecondsLeft.ToString();
                }));

                Thread.Sleep(1000);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Starts the threads to update the times.
        /// </summary>
        private void updateTimes()
        {
            Task.Run(() => updateSeconds());
            Task.Run(() => updateTimesEveryMinute());
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
                Task.Run(() => checkAuthTime());

                MessageBox.Show($"Logged in!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                updateTimes();
           }

           else
           {
                timeUpdates = false;
           }
        }
    }
}
