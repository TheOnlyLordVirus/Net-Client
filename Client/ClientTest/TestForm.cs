using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeyAuthorization;
using System.Diagnostics;

namespace ClientTest
{
    public partial class TestForm : Form
    {
        ClientAuth api = new ClientAuth();
        bool loggedin = false;
        public TestForm()
        {
            InitializeComponent();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            ClientAuth.LoginState loginState = api.Login("pastafarian", "kush007");

            if (loginState.Equals(ClientAuth.LoginState.Logged_In))
            {
                MessageBox.Show($"Logged in!" , "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task.Run(() => checkAuthentication());
            }

            else if(loginState.Equals(ClientAuth.LoginState.Password_Failure))
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

        private void testButton2_Click(object sender, EventArgs e)
        {
            if(api.AuthorizedWithTimeLeft)
            {
                MessageBox.Show
                (
                    $"Years Left: {api.YearsLeft}" +
                    $"\nDays Left: {api.DaysLeft}" +
                    $"\nHours Left: {api.HoursLeft}" +
                    $"\nMinutes Left: {api.MinutesLeft}" +
                    $"\nSeconds Left: {api.SecondsLeft}",
                    "Time Left",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            else
            {
                MessageBox.Show("User isn't authorized with time left.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void testButton3_Click(object sender, EventArgs e)
        {
            if(api.Authorized)
            {
                MessageBox.Show($"Key Redeemed: {api.RedeemKey("3E536-F6E3E-C8C65-941BA")}", "Redeem Key", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("User isn't authorized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Task checkAuthentication()
        {
            while (api.Authorized && api.HeartRate);

            MessageBox.Show("Error", "Authentication to server failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return Task.CompletedTask;
        }
    }
}
