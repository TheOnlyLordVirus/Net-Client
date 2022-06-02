
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
            this.GameCheatTab = new DevExpress.XtraTab.XtraTabPage();
            this.CheatTiles = new DevExpress.XtraEditors.TileControl();
            this.CheatGroup = new DevExpress.XtraEditors.TileGroup();
            this.TimeTab = new DevExpress.XtraTab.XtraTabPage();
            this.TimeLabel = new DevExpress.XtraEditors.LabelControl();
            this.EndDateLabel = new DevExpress.XtraEditors.LabelControl();
            this.TimeCounterLabel = new DevExpress.XtraEditors.LabelControl();
            this.RedeemKeyTab = new DevExpress.XtraTab.XtraTabPage();
            this.hyphenCheck = new DevExpress.XtraEditors.CheckEdit();
            this.RedeemKeyTextbox = new DevExpress.XtraEditors.TextEdit();
            this.hyphen3 = new DevExpress.XtraEditors.LabelControl();
            this.hyphen2 = new DevExpress.XtraEditors.LabelControl();
            this.hyphen1 = new DevExpress.XtraEditors.LabelControl();
            this.RedeemKeyTextbox4 = new DevExpress.XtraEditors.TextEdit();
            this.RedeemKeyTextbox3 = new DevExpress.XtraEditors.TextEdit();
            this.RedeemKeyTextbox2 = new DevExpress.XtraEditors.TextEdit();
            this.RedeemKeyLabel = new DevExpress.XtraEditors.LabelControl();
            this.RedeemKeyButton = new DevExpress.XtraEditors.SimpleButton();
            this.RedeemKeyTextbox1 = new DevExpress.XtraEditors.TextEdit();
            this.LoginTab = new DevExpress.XtraTab.XtraTabPage();
            this.LoginLabel = new DevExpress.XtraEditors.LabelControl();
            this.PasswordLabel = new DevExpress.XtraEditors.LabelControl();
            this.UsernameLabel = new DevExpress.XtraEditors.LabelControl();
            this.UsernameTextbox = new DevExpress.XtraEditors.TextEdit();
            this.PasswordTextbox = new DevExpress.XtraEditors.TextEdit();
            this.LoginButton = new DevExpress.XtraEditors.SimpleButton();
            this.RegisterTab = new DevExpress.XtraTab.XtraTabPage();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.RegisterEmailTextbox = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.RegisterUsernameTextbox = new DevExpress.XtraEditors.TextEdit();
            this.RegisterPasswordTextbox = new DevExpress.XtraEditors.TextEdit();
            this.RegisterButton = new DevExpress.XtraEditors.SimpleButton();
            this.MainTab = new DevExpress.XtraTab.XtraTabControl();
            this.GameCheatTab.SuspendLayout();
            this.TimeTab.SuspendLayout();
            this.RedeemKeyTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hyphenCheck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox1.Properties)).BeginInit();
            this.LoginTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UsernameTextbox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTextbox.Properties)).BeginInit();
            this.RegisterTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RegisterEmailTextbox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegisterUsernameTextbox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegisterPasswordTextbox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainTab)).BeginInit();
            this.MainTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameCheatTab
            // 
            this.GameCheatTab.Controls.Add(this.CheatTiles);
            this.GameCheatTab.Margin = new System.Windows.Forms.Padding(40, 44, 40, 44);
            this.GameCheatTab.Name = "GameCheatTab";
            this.GameCheatTab.PageEnabled = false;
            this.GameCheatTab.Size = new System.Drawing.Size(763, 577);
            this.GameCheatTab.Text = "Cheats";
            // 
            // CheatTiles
            // 
            this.CheatTiles.Groups.Add(this.CheatGroup);
            this.CheatTiles.ItemPadding = new System.Windows.Forms.Padding(10);
            this.CheatTiles.Location = new System.Drawing.Point(0, 0);
            this.CheatTiles.Margin = new System.Windows.Forms.Padding(40, 44, 40, 44);
            this.CheatTiles.Name = "CheatTiles";
            this.CheatTiles.Padding = new System.Windows.Forms.Padding(0);
            this.CheatTiles.Size = new System.Drawing.Size(773, 578);
            this.CheatTiles.TabIndex = 2;
            // 
            // CheatGroup
            // 
            this.CheatGroup.Name = "CheatGroup";
            // 
            // TimeTab
            // 
            this.TimeTab.Controls.Add(this.TimeLabel);
            this.TimeTab.Controls.Add(this.EndDateLabel);
            this.TimeTab.Controls.Add(this.TimeCounterLabel);
            this.TimeTab.Margin = new System.Windows.Forms.Padding(80, 88, 80, 88);
            this.TimeTab.Name = "TimeTab";
            this.TimeTab.PageEnabled = false;
            this.TimeTab.Size = new System.Drawing.Size(763, 577);
            this.TimeTab.Text = "Time Left";
            // 
            // TimeLabel
            // 
            this.TimeLabel.Appearance.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.TimeLabel.Appearance.Options.UseFont = true;
            this.TimeLabel.Appearance.Options.UseForeColor = true;
            this.TimeLabel.Location = new System.Drawing.Point(226, 80);
            this.TimeLabel.Margin = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(295, 67);
            this.TimeLabel.TabIndex = 13;
            this.TimeLabel.Text = "Time Left";
            // 
            // EndDateLabel
            // 
            this.EndDateLabel.Appearance.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndDateLabel.Appearance.Options.UseFont = true;
            this.EndDateLabel.Location = new System.Drawing.Point(8, 535);
            this.EndDateLabel.Margin = new System.Windows.Forms.Padding(147, 177, 147, 177);
            this.EndDateLabel.Name = "EndDateLabel";
            this.EndDateLabel.Size = new System.Drawing.Size(235, 34);
            this.EndDateLabel.TabIndex = 5;
            this.EndDateLabel.Text = "Expiration Date:";
            // 
            // TimeCounterLabel
            // 
            this.TimeCounterLabel.Appearance.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeCounterLabel.Appearance.Options.UseFont = true;
            this.TimeCounterLabel.Location = new System.Drawing.Point(269, 274);
            this.TimeCounterLabel.Margin = new System.Windows.Forms.Padding(147, 177, 147, 177);
            this.TimeCounterLabel.Name = "TimeCounterLabel";
            this.TimeCounterLabel.Size = new System.Drawing.Size(236, 34);
            this.TimeCounterLabel.TabIndex = 1;
            this.TimeCounterLabel.Text = "Loading Times...";
            // 
            // RedeemKeyTab
            // 
            this.RedeemKeyTab.Controls.Add(this.hyphenCheck);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyTextbox);
            this.RedeemKeyTab.Controls.Add(this.hyphen3);
            this.RedeemKeyTab.Controls.Add(this.hyphen2);
            this.RedeemKeyTab.Controls.Add(this.hyphen1);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyTextbox4);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyTextbox3);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyTextbox2);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyLabel);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyButton);
            this.RedeemKeyTab.Controls.Add(this.RedeemKeyTextbox1);
            this.RedeemKeyTab.Margin = new System.Windows.Forms.Padding(80, 88, 80, 88);
            this.RedeemKeyTab.Name = "RedeemKeyTab";
            this.RedeemKeyTab.PageEnabled = false;
            this.RedeemKeyTab.Size = new System.Drawing.Size(763, 577);
            this.RedeemKeyTab.Text = "Redeem Key";
            // 
            // hyphenCheck
            // 
            this.hyphenCheck.EditValue = true;
            this.hyphenCheck.Location = new System.Drawing.Point(177, 316);
            this.hyphenCheck.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.hyphenCheck.Name = "hyphenCheck";
            this.hyphenCheck.Properties.Caption = "Show Hyphens?";
            this.hyphenCheck.Size = new System.Drawing.Size(198, 32);
            this.hyphenCheck.TabIndex = 6;
            this.hyphenCheck.CheckedChanged += new System.EventHandler(this.hyphenCheck_CheckedChanged);
            // 
            // RedeemKeyTextbox
            // 
            this.RedeemKeyTextbox.Enabled = false;
            this.RedeemKeyTextbox.Location = new System.Drawing.Point(177, 270);
            this.RedeemKeyTextbox.Margin = new System.Windows.Forms.Padding(147, 177, 147, 177);
            this.RedeemKeyTextbox.Name = "RedeemKeyTextbox";
            this.RedeemKeyTextbox.Properties.MaxLength = 23;
            this.RedeemKeyTextbox.Size = new System.Drawing.Size(430, 38);
            this.RedeemKeyTextbox.TabIndex = 4;
            this.RedeemKeyTextbox.Visible = false;
            // 
            // hyphen3
            // 
            this.hyphen3.Location = new System.Drawing.Point(504, 276);
            this.hyphen3.Margin = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.hyphen3.Name = "hyphen3";
            this.hyphen3.Size = new System.Drawing.Size(7, 23);
            this.hyphen3.TabIndex = 18;
            this.hyphen3.Text = "-";
            // 
            // hyphen2
            // 
            this.hyphen2.Location = new System.Drawing.Point(389, 276);
            this.hyphen2.Margin = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.hyphen2.Name = "hyphen2";
            this.hyphen2.Size = new System.Drawing.Size(7, 23);
            this.hyphen2.TabIndex = 17;
            this.hyphen2.Text = "-";
            // 
            // hyphen1
            // 
            this.hyphen1.Location = new System.Drawing.Point(272, 276);
            this.hyphen1.Margin = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.hyphen1.Name = "hyphen1";
            this.hyphen1.Size = new System.Drawing.Size(7, 23);
            this.hyphen1.TabIndex = 16;
            this.hyphen1.Text = "-";
            // 
            // RedeemKeyTextbox4
            // 
            this.RedeemKeyTextbox4.Location = new System.Drawing.Point(525, 270);
            this.RedeemKeyTextbox4.Margin = new System.Windows.Forms.Padding(293, 353, 293, 353);
            this.RedeemKeyTextbox4.Name = "RedeemKeyTextbox4";
            this.RedeemKeyTextbox4.Properties.MaxLength = 5;
            this.RedeemKeyTextbox4.Size = new System.Drawing.Size(82, 38);
            this.RedeemKeyTextbox4.TabIndex = 3;
            // 
            // RedeemKeyTextbox3
            // 
            this.RedeemKeyTextbox3.Location = new System.Drawing.Point(409, 270);
            this.RedeemKeyTextbox3.Margin = new System.Windows.Forms.Padding(293, 353, 293, 353);
            this.RedeemKeyTextbox3.Name = "RedeemKeyTextbox3";
            this.RedeemKeyTextbox3.Properties.MaxLength = 5;
            this.RedeemKeyTextbox3.Size = new System.Drawing.Size(82, 38);
            this.RedeemKeyTextbox3.TabIndex = 2;
            // 
            // RedeemKeyTextbox2
            // 
            this.RedeemKeyTextbox2.Location = new System.Drawing.Point(293, 270);
            this.RedeemKeyTextbox2.Margin = new System.Windows.Forms.Padding(293, 353, 293, 353);
            this.RedeemKeyTextbox2.Name = "RedeemKeyTextbox2";
            this.RedeemKeyTextbox2.Properties.MaxLength = 5;
            this.RedeemKeyTextbox2.Size = new System.Drawing.Size(82, 38);
            this.RedeemKeyTextbox2.TabIndex = 1;
            // 
            // RedeemKeyLabel
            // 
            this.RedeemKeyLabel.Appearance.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedeemKeyLabel.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.RedeemKeyLabel.Appearance.Options.UseFont = true;
            this.RedeemKeyLabel.Appearance.Options.UseForeColor = true;
            this.RedeemKeyLabel.Location = new System.Drawing.Point(192, 121);
            this.RedeemKeyLabel.Margin = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.RedeemKeyLabel.Name = "RedeemKeyLabel";
            this.RedeemKeyLabel.Size = new System.Drawing.Size(388, 67);
            this.RedeemKeyLabel.TabIndex = 12;
            this.RedeemKeyLabel.Text = "Redeem Key";
            // 
            // RedeemKeyButton
            // 
            this.RedeemKeyButton.Location = new System.Drawing.Point(254, 367);
            this.RedeemKeyButton.Margin = new System.Windows.Forms.Padding(1187, 1413, 1187, 1413);
            this.RedeemKeyButton.Name = "RedeemKeyButton";
            this.RedeemKeyButton.Size = new System.Drawing.Size(277, 78);
            this.RedeemKeyButton.TabIndex = 5;
            this.RedeemKeyButton.Text = "Redeem";
            this.RedeemKeyButton.Click += new System.EventHandler(this.RedeemKeyButton_Click);
            // 
            // RedeemKeyTextbox1
            // 
            this.RedeemKeyTextbox1.Location = new System.Drawing.Point(177, 270);
            this.RedeemKeyTextbox1.Margin = new System.Windows.Forms.Padding(147, 177, 147, 177);
            this.RedeemKeyTextbox1.Name = "RedeemKeyTextbox1";
            this.RedeemKeyTextbox1.Properties.MaxLength = 5;
            this.RedeemKeyTextbox1.Size = new System.Drawing.Size(82, 38);
            this.RedeemKeyTextbox1.TabIndex = 0;
            // 
            // LoginTab
            // 
            this.LoginTab.Controls.Add(this.LoginLabel);
            this.LoginTab.Controls.Add(this.PasswordLabel);
            this.LoginTab.Controls.Add(this.UsernameLabel);
            this.LoginTab.Controls.Add(this.UsernameTextbox);
            this.LoginTab.Controls.Add(this.PasswordTextbox);
            this.LoginTab.Controls.Add(this.LoginButton);
            this.LoginTab.Margin = new System.Windows.Forms.Padding(80, 88, 80, 88);
            this.LoginTab.Name = "LoginTab";
            this.LoginTab.Size = new System.Drawing.Size(763, 577);
            this.LoginTab.Text = "Login";
            // 
            // LoginLabel
            // 
            this.LoginLabel.Appearance.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginLabel.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.LoginLabel.Appearance.Options.UseFont = true;
            this.LoginLabel.Appearance.Options.UseForeColor = true;
            this.LoginLabel.Location = new System.Drawing.Point(293, 121);
            this.LoginLabel.Margin = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(172, 67);
            this.LoginLabel.TabIndex = 11;
            this.LoginLabel.Text = "Login";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.Location = new System.Drawing.Point(86, 294);
            this.PasswordLabel.Margin = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(85, 23);
            this.PasswordLabel.TabIndex = 10;
            this.PasswordLabel.Text = "Password:";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.Location = new System.Drawing.Point(80, 246);
            this.UsernameLabel.Margin = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(91, 23);
            this.UsernameLabel.TabIndex = 9;
            this.UsernameLabel.Text = "Username:";
            // 
            // UsernameTextbox
            // 
            this.UsernameTextbox.EditValue = "";
            this.UsernameTextbox.Location = new System.Drawing.Point(182, 240);
            this.UsernameTextbox.Margin = new System.Windows.Forms.Padding(587, 707, 587, 707);
            this.UsernameTextbox.Name = "UsernameTextbox";
            this.UsernameTextbox.Size = new System.Drawing.Size(414, 38);
            this.UsernameTextbox.TabIndex = 0;
            // 
            // PasswordTextbox
            // 
            this.PasswordTextbox.Location = new System.Drawing.Point(182, 288);
            this.PasswordTextbox.Margin = new System.Windows.Forms.Padding(587, 707, 587, 707);
            this.PasswordTextbox.Name = "PasswordTextbox";
            this.PasswordTextbox.Properties.PasswordChar = '*';
            this.PasswordTextbox.Properties.UseSystemPasswordChar = true;
            this.PasswordTextbox.Size = new System.Drawing.Size(414, 38);
            this.PasswordTextbox.TabIndex = 1;
            this.PasswordTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PasswordTextbox_KeyDown);
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(251, 367);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(587, 707, 587, 707);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(277, 78);
            this.LoginButton.TabIndex = 2;
            this.LoginButton.Text = "Login";
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // RegisterTab
            // 
            this.RegisterTab.Controls.Add(this.label4);
            this.RegisterTab.Controls.Add(this.RegisterEmailTextbox);
            this.RegisterTab.Controls.Add(this.label1);
            this.RegisterTab.Controls.Add(this.label2);
            this.RegisterTab.Controls.Add(this.label3);
            this.RegisterTab.Controls.Add(this.RegisterUsernameTextbox);
            this.RegisterTab.Controls.Add(this.RegisterPasswordTextbox);
            this.RegisterTab.Controls.Add(this.RegisterButton);
            this.RegisterTab.Margin = new System.Windows.Forms.Padding(40, 44, 40, 44);
            this.RegisterTab.Name = "RegisterTab";
            this.RegisterTab.Size = new System.Drawing.Size(763, 577);
            this.RegisterTab.Text = "Register";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(116, 214);
            this.label4.Margin = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 23);
            this.label4.TabIndex = 19;
            this.label4.Text = "Email:";
            // 
            // RegisterEmailTextbox
            // 
            this.RegisterEmailTextbox.Location = new System.Drawing.Point(182, 208);
            this.RegisterEmailTextbox.Margin = new System.Windows.Forms.Padding(587, 707, 587, 707);
            this.RegisterEmailTextbox.Name = "RegisterEmailTextbox";
            this.RegisterEmailTextbox.Size = new System.Drawing.Size(414, 38);
            this.RegisterEmailTextbox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Appearance.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.label1.Appearance.Options.UseFont = true;
            this.label1.Appearance.Options.UseForeColor = true;
            this.label1.Location = new System.Drawing.Point(250, 121);
            this.label1.Margin = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 67);
            this.label1.TabIndex = 17;
            this.label1.Text = "Register";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(85, 311);
            this.label2.Margin = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "Password:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(80, 262);
            this.label3.Margin = new System.Windows.Forms.Padding(80, 0, 80, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 23);
            this.label3.TabIndex = 15;
            this.label3.Text = "Username:";
            // 
            // RegisterUsernameTextbox
            // 
            this.RegisterUsernameTextbox.Location = new System.Drawing.Point(182, 257);
            this.RegisterUsernameTextbox.Margin = new System.Windows.Forms.Padding(587, 707, 587, 707);
            this.RegisterUsernameTextbox.Name = "RegisterUsernameTextbox";
            this.RegisterUsernameTextbox.Size = new System.Drawing.Size(414, 38);
            this.RegisterUsernameTextbox.TabIndex = 1;
            // 
            // RegisterPasswordTextbox
            // 
            this.RegisterPasswordTextbox.Location = new System.Drawing.Point(182, 305);
            this.RegisterPasswordTextbox.Margin = new System.Windows.Forms.Padding(587, 707, 587, 707);
            this.RegisterPasswordTextbox.Name = "RegisterPasswordTextbox";
            this.RegisterPasswordTextbox.Properties.PasswordChar = '*';
            this.RegisterPasswordTextbox.Properties.UseSystemPasswordChar = true;
            this.RegisterPasswordTextbox.Size = new System.Drawing.Size(414, 38);
            this.RegisterPasswordTextbox.TabIndex = 2;
            this.RegisterPasswordTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegisterPasswordTextbox_KeyDown);
            // 
            // RegisterButton
            // 
            this.RegisterButton.Location = new System.Drawing.Point(251, 367);
            this.RegisterButton.Margin = new System.Windows.Forms.Padding(587, 707, 587, 707);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(277, 78);
            this.RegisterButton.TabIndex = 3;
            this.RegisterButton.Text = "Register";
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // MainTab
            // 
            this.MainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTab.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.MainTab.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.MainTab.Location = new System.Drawing.Point(0, 0);
            this.MainTab.Margin = new System.Windows.Forms.Padding(147, 177, 147, 177);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedTabPage = this.LoginTab;
            this.MainTab.Size = new System.Drawing.Size(895, 581);
            this.MainTab.TabIndex = 6;
            this.MainTab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.RegisterTab,
            this.LoginTab,
            this.RedeemKeyTab,
            this.TimeTab,
            this.GameCheatTab});
            this.MainTab.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.MainTab_SelectedPageChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 581);
            this.Controls.Add(this.MainTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.SvgImage = global::NetClient.Properties.Resources.charttype_radarline;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MainForm";
            this.Text = "Net Client";
            this.GameCheatTab.ResumeLayout(false);
            this.TimeTab.ResumeLayout(false);
            this.TimeTab.PerformLayout();
            this.RedeemKeyTab.ResumeLayout(false);
            this.RedeemKeyTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hyphenCheck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedeemKeyTextbox1.Properties)).EndInit();
            this.LoginTab.ResumeLayout(false);
            this.LoginTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UsernameTextbox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTextbox.Properties)).EndInit();
            this.RegisterTab.ResumeLayout(false);
            this.RegisterTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RegisterEmailTextbox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegisterUsernameTextbox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RegisterPasswordTextbox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainTab)).EndInit();
            this.MainTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabPage GameCheatTab;
        private DevExpress.XtraTab.XtraTabPage TimeTab;
        private DevExpress.XtraEditors.LabelControl TimeLabel;
        private DevExpress.XtraEditors.LabelControl EndDateLabel;
        private DevExpress.XtraEditors.LabelControl TimeCounterLabel;
        private DevExpress.XtraTab.XtraTabPage RedeemKeyTab;
        private DevExpress.XtraEditors.LabelControl hyphen3;
        private DevExpress.XtraEditors.LabelControl hyphen2;
        private DevExpress.XtraEditors.LabelControl hyphen1;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox4;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox3;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox2;
        private DevExpress.XtraEditors.LabelControl RedeemKeyLabel;
        private DevExpress.XtraEditors.SimpleButton RedeemKeyButton;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox1;
        private DevExpress.XtraTab.XtraTabPage LoginTab;
        private DevExpress.XtraEditors.LabelControl LoginLabel;
        private DevExpress.XtraEditors.LabelControl PasswordLabel;
        private DevExpress.XtraEditors.LabelControl UsernameLabel;
        private DevExpress.XtraEditors.TextEdit UsernameTextbox;
        private DevExpress.XtraEditors.TextEdit PasswordTextbox;
        private DevExpress.XtraEditors.SimpleButton LoginButton;
        private DevExpress.XtraTab.XtraTabPage RegisterTab;
        private DevExpress.XtraEditors.LabelControl label1;
        private DevExpress.XtraEditors.LabelControl label2;
        private DevExpress.XtraEditors.LabelControl label3;
        private DevExpress.XtraEditors.TextEdit RegisterUsernameTextbox;
        private DevExpress.XtraEditors.TextEdit RegisterPasswordTextbox;
        private DevExpress.XtraEditors.SimpleButton RegisterButton;
        private DevExpress.XtraTab.XtraTabControl MainTab;
        private DevExpress.XtraEditors.LabelControl label4;
        private DevExpress.XtraEditors.TextEdit RegisterEmailTextbox;
        private DevExpress.XtraEditors.TileControl CheatTiles;
        private DevExpress.XtraEditors.TileGroup CheatGroup;
        private DevExpress.XtraEditors.CheckEdit hyphenCheck;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox;
    }
}

