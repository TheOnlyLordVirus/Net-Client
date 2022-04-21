using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace testcheat
{
    public partial class MainForm : XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SampleEvent_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello!", "Debug");
        }
    }
}
