﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientAuth;
using System.Diagnostics;

namespace ClientTest
{
    public partial class TestForm : Form
    {
        Auth api = new Auth();
        bool loggedin = false;
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
                loggedin = true;
            }

            else
            {
                MessageBox.Show("Error", "Failed to login!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void testButton2_Click(object sender, EventArgs e)
        {
            if(api.AuthorizedWithTimeLeft & loggedin)
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
            if(api.Authorized && loggedin)
            {
                MessageBox.Show($"Key Redeemed: {api.redeemKey("3E536-F6E3E-C8C65-941BA")}", "Redeem Key", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            loggedin = false;
            return Task.CompletedTask;
        }
    }
}
