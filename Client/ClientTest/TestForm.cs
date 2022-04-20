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
        ClientAuth AuthApi = new ClientAuth();
        public TestForm()
        {
            InitializeComponent();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            ClientAuth.LoginState loginState = AuthApi.Login("pastafarian", "cheesetoast");

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
            if(AuthApi.AuthorizedWithTimeLeft)
            {
                MessageBox.Show
                (
                    $"Years Left: {AuthApi.YearsLeft}" +
                    $"\nDays Left: {AuthApi.DaysLeft}" +
                    $"\nHours Left: {AuthApi.HoursLeft}" +
                    $"\nMinutes Left: {AuthApi.MinutesLeft}" +
                    $"\nSeconds Left: {AuthApi.SecondsLeft}",
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
            if(AuthApi.Authorized)
            {
                MessageBox.Show($"Key Redeemed: {AuthApi.RedeemKey("3E536-F6E3E-C8C65-941BA")}", "Redeem Key", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("User isn't authorized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Task checkAuthentication()
        {
            while (AuthApi.Authorized);

            MessageBox.Show("Error", "Authentication to server failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return Task.CompletedTask;
        }
    }
}
