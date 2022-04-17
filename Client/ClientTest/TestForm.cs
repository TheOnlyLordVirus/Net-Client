using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientTest.lib;
using System.Diagnostics;

namespace ClientTest
{
    public partial class TestForm : Form
    {
        Auth api = new Auth();
        public TestForm()
        {
            InitializeComponent();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            if (api.login("pastafarian", "cheesetoast"))
            {
                MessageBox.Show($"Logged in!" , "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task.Run(() => checkAuthentication());
            }

            else
            {
                MessageBox.Show("Error", "Failed to login!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Key Redeemed: {api.redeemKey("12345-12345-12345-12345-54321")}", "Redeem Key", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("User isn't authorized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Task checkAuthentication()
        {
            while (api.Authorized)
            {
                if (api.HeartRate)
                {
                    Debugger.Log(1, "Test:", "Something happened with auth");
                }
            }
            MessageBox.Show("Error", "Authentication to server failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return Task.CompletedTask;
        }
    }
}
