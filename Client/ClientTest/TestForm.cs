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
            //Debugger.Log(1, "Test:", api.login("pastafarian", "cheesetoast").ToString()); 
            if(api.login("pastafarian", "cheesetoast"))
            {
                MessageBox.Show("Success", "Logged in!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task.Run(() => checkAuthentication());
            }

            else
            {
                MessageBox.Show("Error", "Failed to login!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void testButton2_Click(object sender, EventArgs e)
        {
            if (api.checkAuthenticationTime())
            {
                MessageBox.Show("Time left", "true", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task.Run(() => checkAuthentication());
            }

            else
            {
                MessageBox.Show("Error", "You have no time left!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Task checkAuthentication()
        {
            while (api.Authorized) ;
            MessageBox.Show("Error", "Authentication to server failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return Task.CompletedTask;
        }
    }
}
