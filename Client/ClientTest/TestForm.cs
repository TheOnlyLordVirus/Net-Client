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
            Debugger.Log(1, "Test:", api.login("pastafarian", "cheesetoast").ToString()); 
        }

        private void testButton2_Click(object sender, EventArgs e)
        {
            Debugger.Log(1, "Test:", api.login("pastafarian", "sex").ToString());
        }
    }
}
