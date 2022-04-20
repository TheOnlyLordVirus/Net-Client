using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KeyAuthorization;

namespace NetClient
{
    public partial class MainForm : XtraForm
    {
        private bool timeUpdates = false;
        private ClientAuth ClientAuthenticator;
        public MainForm()
        {
            InitializeComponent();
            ClientAuthenticator = new ClientAuth();
        }

        /// <summary>
        /// Show the client an error if we fail auth.
        /// </summary>
        /// <returns></returns>
        private Task checkAuthentication()
        {
            while (ClientAuthenticator.Authorized) ;
            MessageBox.Show("Error", "Authentication to server failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            TimeTab.PageEnabled = false;
            RedeemKeyTab.PageEnabled = false;
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
            ClientAuth.LoginState loginState = ClientAuthenticator.Login(UsernameTextbox.Text, PasswordTextbox.Text);

            if (loginState.Equals(ClientAuth.LoginState.Logged_In))
            {
                MessageBox.Show($"Logged in!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task.Run(() => checkAuthentication());
                TimeTab.PageEnabled = true;
                RedeemKeyTab.PageEnabled = true;
            }

            else if (loginState.Equals(ClientAuth.LoginState.Password_Failure))
            {
                MessageBox.Show("Password Mismatch failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (loginState.Equals(ClientAuth.LoginState.IP_Mismatch))
            {
                MessageBox.Show("User IP Address Mismatch failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (loginState.Equals(ClientAuth.LoginState.User_doesnt_Exist))
            {
                MessageBox.Show("User doesnt exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (loginState.Equals(ClientAuth.LoginState.Response_Error))
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
