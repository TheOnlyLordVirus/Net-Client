
namespace NetClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainTab = new DevExpress.XtraTab.XtraTabControl();
            this.LoginTab = new DevExpress.XtraTab.XtraTabPage();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.UsernameTextbox = new DevExpress.XtraEditors.TextEdit();
            this.PasswordTextbox = new DevExpress.XtraEditors.TextEdit();
            this.LoginButton = new DevExpress.XtraEditors.SimpleButton();
            this.RedeemKeyTab = new DevExpress.XtraTab.XtraTabPage();
            this.hyphen3 = new System.Windows.Forms.Label();
            this.hyphen2 = new System.Windows.Forms.Label();
            this.hyphen1 = new System.Windows.Forms.Label();
            this.RedeemKeyTextbox4 = new DevExpress.XtraEditors.TextEdit();
            this.RedeemKeyTextbox3 = new DevExpress.XtraEditors.TextEdit();
            this.RedeemKeyTextbox2 = new DevExpress.XtraEditors.TextEdit();
            this.RedeemKeyLabel = new System.Windows.Forms.Label();
            this.RedeemKeyButton = new DevExpress.XtraEditors.SimpleButton();
            this.RedeemKeyTextbox1 = new DevExpress.XtraEditors.TextEdit();
            this.TimeTab = new DevExpress.XtraTab.XtraTabPage();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.SecondsLabel = new DevExpress.XtraEditors.LabelControl();
            this.MinuteLabel = new DevExpress.XtraEditors.LabelControl();
            this.HourLabel = new DevExpress.XtraEditors.LabelControl();
            this.DayLabel = new DevExpress.XtraEditors.LabelControl();
            this.MonthLabel = new DevExpress.XtraEditors.LabelControl();
            this.YearLabel = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.MainTab)).BeginInit();
            this.MainTab.SuspendLayout();
            this.LoginTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UsernameTextbox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTextbox.Properties)).BeginInit();
            this.RedeemKeyTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox1.Properties)).BeginInit();
            this.TimeTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTab
            // 
            this.MainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTab.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.MainTab.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.MainTab.Location = new System.Drawing.Point(0, 0);
            this.MainTab.Margin = new System.Windows.Forms.Padding(12);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedTabPage = this.LoginTab;
            this.MainTab.Size = new System.Drawing.Size(1163, 632);
            this.MainTab.TabIndex = 6;
            this.MainTab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.LoginTab,
            this.RedeemKeyTab,
            this.TimeTab});
            this.MainTab.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.MainTab_SelectedPageChanged);
            // 
            // LoginTab
            // 
            this.LoginTab.Controls.Add(this.LoginLabel);
            this.LoginTab.Controls.Add(this.PasswordLabel);
            this.LoginTab.Controls.Add(this.UsernameLabel);
            this.LoginTab.Controls.Add(this.UsernameTextbox);
            this.LoginTab.Controls.Add(this.PasswordTextbox);
            this.LoginTab.Controls.Add(this.LoginButton);
            this.LoginTab.Margin = new System.Windows.Forms.Padding(6);
            this.LoginTab.Name = "LoginTab";
            this.LoginTab.Size = new System.Drawing.Size(999, 628);
            this.LoginTab.Text = "Login";
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginLabel.ForeColor = System.Drawing.Color.Gray;
            this.LoginLabel.Location = new System.Drawing.Point(404, 100);
            this.LoginLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(231, 78);
            this.LoginLabel.TabIndex = 11;
            this.LoginLabel.Text = "Login";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(109, 290);
            this.PasswordLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(121, 25);
            this.PasswordLabel.TabIndex = 10;
            this.PasswordLabel.Text = "Password:";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(101, 232);
            this.UsernameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(129, 25);
            this.UsernameLabel.TabIndex = 9;
            this.UsernameLabel.Text = "Username:";
            // 
            // UsernameTextbox
            // 
            this.UsernameTextbox.Location = new System.Drawing.Point(239, 226);
            this.UsernameTextbox.Margin = new System.Windows.Forms.Padding(48);
            this.UsernameTextbox.Name = "UsernameTextbox";
            this.UsernameTextbox.Size = new System.Drawing.Size(560, 40);
            this.UsernameTextbox.TabIndex = 8;
            // 
            // PasswordTextbox
            // 
            this.PasswordTextbox.Location = new System.Drawing.Point(239, 284);
            this.PasswordTextbox.Margin = new System.Windows.Forms.Padding(48);
            this.PasswordTextbox.Name = "PasswordTextbox";
            this.PasswordTextbox.Properties.PasswordChar = '*';
            this.PasswordTextbox.Properties.UseSystemPasswordChar = true;
            this.PasswordTextbox.Size = new System.Drawing.Size(560, 40);
            this.PasswordTextbox.TabIndex = 7;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(313, 362);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(48);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(413, 87);
            this.LoginButton.TabIndex = 6;
            this.LoginButton.Text = "Login";
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // RedeemKeyTab
            // 
            this.RedeemKeyTab.Controls.Add(this.hyphen3);
            this.RedeemKeyTab.Controls.Add(this.hyphen2);
            this.RedeemKeyTab.Controls.Add(this.hyphen1);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyTextbox4);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyTextbox3);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyTextbox2);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyLabel);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyButton);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyTextbox1);
            this.RedeemKeyTab.Margin = new System.Windows.Forms.Padding(6);
            this.RedeemKeyTab.Name = "RedeemKeyTab";
            this.RedeemKeyTab.PageEnabled = false;
            this.RedeemKeyTab.Size = new System.Drawing.Size(999, 628);
            this.RedeemKeyTab.Text = "Redeem Key";
            // 
            // hyphen3
            // 
            this.hyphen3.AutoSize = true;
            this.hyphen3.Location = new System.Drawing.Point(681, 268);
            this.hyphen3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.hyphen3.Name = "hyphen3";
            this.hyphen3.Size = new System.Drawing.Size(22, 25);
            this.hyphen3.TabIndex = 18;
            this.hyphen3.Text = "-";
            // 
            // hyphen2
            // 
            this.hyphen2.AutoSize = true;
            this.hyphen2.Location = new System.Drawing.Point(488, 268);
            this.hyphen2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.hyphen2.Name = "hyphen2";
            this.hyphen2.Size = new System.Drawing.Size(22, 25);
            this.hyphen2.TabIndex = 17;
            this.hyphen2.Text = "-";
            // 
            // hyphen1
            // 
            this.hyphen1.AutoSize = true;
            this.hyphen1.Location = new System.Drawing.Point(301, 268);
            this.hyphen1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.hyphen1.Name = "hyphen1";
            this.hyphen1.Size = new System.Drawing.Size(22, 25);
            this.hyphen1.TabIndex = 16;
            this.hyphen1.Text = "-";
            // 
            // RedeemKeyTextbox4
            // 
            this.RedeemKeyTextbox4.Location = new System.Drawing.Point(718, 261);
            this.RedeemKeyTextbox4.Margin = new System.Windows.Forms.Padding(24);
            this.RedeemKeyTextbox4.Name = "RedeemKeyTextbox4";
            this.RedeemKeyTextbox4.Size = new System.Drawing.Size(138, 40);
            this.RedeemKeyTextbox4.TabIndex = 15;
            // 
            // RedeemKeyTextbox3
            // 
            this.RedeemKeyTextbox3.Location = new System.Drawing.Point(527, 261);
            this.RedeemKeyTextbox3.Margin = new System.Windows.Forms.Padding(24);
            this.RedeemKeyTextbox3.Name = "RedeemKeyTextbox3";
            this.RedeemKeyTextbox3.Size = new System.Drawing.Size(138, 40);
            this.RedeemKeyTextbox3.TabIndex = 14;
            // 
            // RedeemKeyTextbox2
            // 
            this.RedeemKeyTextbox2.Location = new System.Drawing.Point(336, 261);
            this.RedeemKeyTextbox2.Margin = new System.Windows.Forms.Padding(24);
            this.RedeemKeyTextbox2.Name = "RedeemKeyTextbox2";
            this.RedeemKeyTextbox2.Size = new System.Drawing.Size(138, 40);
            this.RedeemKeyTextbox2.TabIndex = 13;
            // 
            // RedeemKeyLabel
            // 
            this.RedeemKeyLabel.AutoSize = true;
            this.RedeemKeyLabel.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedeemKeyLabel.ForeColor = System.Drawing.Color.Gray;
            this.RedeemKeyLabel.Location = new System.Drawing.Point(251, 133);
            this.RedeemKeyLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.RedeemKeyLabel.Name = "RedeemKeyLabel";
            this.RedeemKeyLabel.Size = new System.Drawing.Size(481, 78);
            this.RedeemKeyLabel.TabIndex = 12;
            this.RedeemKeyLabel.Text = "Redeem Key";
            // 
            // RedeemKeyButton
            // 
            this.RedeemKeyButton.Location = new System.Drawing.Point(264, 357);
            this.RedeemKeyButton.Margin = new System.Windows.Forms.Padding(96);
            this.RedeemKeyButton.Name = "RedeemKeyButton";
            this.RedeemKeyButton.Size = new System.Drawing.Size(451, 93);
            this.RedeemKeyButton.TabIndex = 7;
            this.RedeemKeyButton.Text = "Redeem";
            this.RedeemKeyButton.Click += new System.EventHandler(this.RedeemKeyButton_Click);
            // 
            // RedeemKeyTextbox1
            // 
            this.RedeemKeyTextbox1.Location = new System.Drawing.Point(145, 261);
            this.RedeemKeyTextbox1.Margin = new System.Windows.Forms.Padding(12);
            this.RedeemKeyTextbox1.Name = "RedeemKeyTextbox1";
            this.RedeemKeyTextbox1.Size = new System.Drawing.Size(138, 40);
            this.RedeemKeyTextbox1.TabIndex = 0;
            // 
            // TimeTab
            // 
            this.TimeTab.Controls.Add(this.TimeLabel);
            this.TimeTab.Controls.Add(this.SecondsLabel);
            this.TimeTab.Controls.Add(this.MinuteLabel);
            this.TimeTab.Controls.Add(this.HourLabel);
            this.TimeTab.Controls.Add(this.DayLabel);
            this.TimeTab.Controls.Add(this.MonthLabel);
            this.TimeTab.Controls.Add(this.YearLabel);
            this.TimeTab.Margin = new System.Windows.Forms.Padding(6);
            this.TimeTab.Name = "TimeTab";
            this.TimeTab.PageEnabled = false;
            this.TimeTab.Size = new System.Drawing.Size(999, 628);
            this.TimeTab.Text = "Time Left";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.Color.Gray;
            this.TimeLabel.Location = new System.Drawing.Point(303, 76);
            this.TimeLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(372, 78);
            this.TimeLabel.TabIndex = 13;
            this.TimeLabel.Text = "Time Left";
            // 
            // SecondsLabel
            // 
            this.SecondsLabel.Appearance.Font = new System.Drawing.Font("Verdana", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecondsLabel.Appearance.Options.UseFont = true;
            this.SecondsLabel.Location = new System.Drawing.Point(215, 502);
            this.SecondsLabel.Margin = new System.Windows.Forms.Padding(24);
            this.SecondsLabel.Name = "SecondsLabel";
            this.SecondsLabel.Size = new System.Drawing.Size(199, 52);
            this.SecondsLabel.TabIndex = 6;
            this.SecondsLabel.Text = "Seconds:";
            // 
            // MinuteLabel
            // 
            this.MinuteLabel.Appearance.Font = new System.Drawing.Font("Verdana", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinuteLabel.Appearance.Options.UseFont = true;
            this.MinuteLabel.Location = new System.Drawing.Point(215, 441);
            this.MinuteLabel.Margin = new System.Windows.Forms.Padding(12);
            this.MinuteLabel.Name = "MinuteLabel";
            this.MinuteLabel.Size = new System.Drawing.Size(187, 52);
            this.MinuteLabel.TabIndex = 5;
            this.MinuteLabel.Text = "Minutes:";
            // 
            // HourLabel
            // 
            this.HourLabel.Appearance.Font = new System.Drawing.Font("Verdana", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HourLabel.Appearance.Options.UseFont = true;
            this.HourLabel.Location = new System.Drawing.Point(216, 380);
            this.HourLabel.Margin = new System.Windows.Forms.Padding(12);
            this.HourLabel.Name = "HourLabel";
            this.HourLabel.Size = new System.Drawing.Size(145, 52);
            this.HourLabel.TabIndex = 4;
            this.HourLabel.Text = "Hours:";
            // 
            // DayLabel
            // 
            this.DayLabel.Appearance.Font = new System.Drawing.Font("Verdana", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DayLabel.Appearance.Options.UseFont = true;
            this.DayLabel.Location = new System.Drawing.Point(215, 319);
            this.DayLabel.Margin = new System.Windows.Forms.Padding(12);
            this.DayLabel.Name = "DayLabel";
            this.DayLabel.Size = new System.Drawing.Size(126, 52);
            this.DayLabel.TabIndex = 3;
            this.DayLabel.Text = "Days:";
            // 
            // MonthLabel
            // 
            this.MonthLabel.Appearance.Font = new System.Drawing.Font("Verdana", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonthLabel.Appearance.Options.UseFont = true;
            this.MonthLabel.Location = new System.Drawing.Point(215, 258);
            this.MonthLabel.Margin = new System.Windows.Forms.Padding(12);
            this.MonthLabel.Name = "MonthLabel";
            this.MonthLabel.Size = new System.Drawing.Size(175, 52);
            this.MonthLabel.TabIndex = 2;
            this.MonthLabel.Text = "Months:";
            // 
            // YearLabel
            // 
            this.YearLabel.Appearance.Font = new System.Drawing.Font("Verdana", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YearLabel.Appearance.Options.UseFont = true;
            this.YearLabel.Location = new System.Drawing.Point(215, 197);
            this.YearLabel.Margin = new System.Windows.Forms.Padding(12);
            this.YearLabel.Name = "YearLabel";
            this.YearLabel.Size = new System.Drawing.Size(153, 52);
            this.YearLabel.TabIndex = 1;
            this.YearLabel.Text = "Years: ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 632);
            this.Controls.Add(this.MainTab);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("MainForm.IconOptions.SvgImage")));
            this.Name = "MainForm";
            this.Text = "Client Login";
            ((System.ComponentModel.ISupportInitialize)(this.MainTab)).EndInit();
            this.MainTab.ResumeLayout(false);
            this.LoginTab.ResumeLayout(false);
            this.LoginTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UsernameTextbox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTextbox.Properties)).EndInit();
            this.RedeemKeyTab.ResumeLayout(false);
            this.RedeemKeyTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox1.Properties)).EndInit();
            this.TimeTab.ResumeLayout(false);
            this.TimeTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl MainTab;
        private DevExpress.XtraTab.XtraTabPage LoginTab;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private DevExpress.XtraEditors.TextEdit UsernameTextbox;
        private DevExpress.XtraEditors.TextEdit PasswordTextbox;
        private DevExpress.XtraEditors.SimpleButton LoginButton;
        private DevExpress.XtraTab.XtraTabPage RedeemKeyTab;
        private System.Windows.Forms.Label RedeemKeyLabel;
        private DevExpress.XtraEditors.SimpleButton RedeemKeyButton;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox1;
        private DevExpress.XtraTab.XtraTabPage TimeTab;
        private System.Windows.Forms.Label TimeLabel;
        private DevExpress.XtraEditors.LabelControl SecondsLabel;
        private DevExpress.XtraEditors.LabelControl MinuteLabel;
        private DevExpress.XtraEditors.LabelControl HourLabel;
        private DevExpress.XtraEditors.LabelControl DayLabel;
        private DevExpress.XtraEditors.LabelControl MonthLabel;
        private DevExpress.XtraEditors.LabelControl YearLabel;
        private System.Windows.Forms.Label hyphen3;
        private System.Windows.Forms.Label hyphen2;
        private System.Windows.Forms.Label hyphen1;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox4;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox3;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox2;
    }
}

