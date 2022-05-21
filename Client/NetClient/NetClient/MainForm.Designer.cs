
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
            this.TimeLabel = new System.Windows.Forms.Label();
            this.EndDateLabel = new DevExpress.XtraEditors.LabelControl();
            this.TimeCounterLabel = new DevExpress.XtraEditors.LabelControl();
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
            this.LoginTab = new DevExpress.XtraTab.XtraTabPage();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.UsernameTextbox = new DevExpress.XtraEditors.TextEdit();
            this.PasswordTextbox = new DevExpress.XtraEditors.TextEdit();
            this.LoginButton = new DevExpress.XtraEditors.SimpleButton();
            this.RegisterTab = new DevExpress.XtraTab.XtraTabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.RegisterEmailTextbox = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RegisterUsernameTextbox = new DevExpress.XtraEditors.TextEdit();
            this.RegisterPasswordTextbox = new DevExpress.XtraEditors.TextEdit();
            this.RegisterButton = new DevExpress.XtraEditors.SimpleButton();
            this.MainTab = new DevExpress.XtraTab.XtraTabControl();
            this.GameCheatTab.SuspendLayout();
            this.TimeTab.SuspendLayout();
            this.RedeemKeyTab.SuspendLayout();
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
            this.GameCheatTab.Margin = new System.Windows.Forms.Padding(48);
            this.GameCheatTab.Name = "GameCheatTab";
            this.GameCheatTab.PageEnabled = false;
            this.GameCheatTab.Size = new System.Drawing.Size(928, 628);
            this.GameCheatTab.Text = "Cheats";
            // 
            // CheatTiles
            // 
            this.CheatTiles.Groups.Add(this.CheatGroup);
            this.CheatTiles.ItemPadding = new System.Windows.Forms.Padding(10);
            this.CheatTiles.Location = new System.Drawing.Point(0, 0);
            this.CheatTiles.Margin = new System.Windows.Forms.Padding(48);
            this.CheatTiles.Name = "CheatTiles";
            this.CheatTiles.Padding = new System.Windows.Forms.Padding(0);
            this.CheatTiles.Size = new System.Drawing.Size(928, 628);
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
            this.TimeTab.Margin = new System.Windows.Forms.Padding(96);
            this.TimeTab.Name = "TimeTab";
            this.TimeTab.PageEnabled = false;
            this.TimeTab.Size = new System.Drawing.Size(928, 628);
            this.TimeTab.Text = "Time Left";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.Color.Gray;
            this.TimeLabel.Location = new System.Drawing.Point(271, 87);
            this.TimeLabel.Margin = new System.Windows.Forms.Padding(96, 0, 96, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(372, 78);
            this.TimeLabel.TabIndex = 13;
            this.TimeLabel.Text = "Time Left";
            // 
            // EndDateLabel
            // 
            this.EndDateLabel.Appearance.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndDateLabel.Appearance.Options.UseFont = true;
            this.EndDateLabel.Location = new System.Drawing.Point(10, 582);
            this.EndDateLabel.Margin = new System.Windows.Forms.Padding(176, 192, 176, 192);
            this.EndDateLabel.Name = "EndDateLabel";
            this.EndDateLabel.Size = new System.Drawing.Size(252, 38);
            this.EndDateLabel.TabIndex = 5;
            this.EndDateLabel.Text = "End Date Time:";
            // 
            // TimeCounterLabel
            // 
            this.TimeCounterLabel.Appearance.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeCounterLabel.Appearance.Options.UseFont = true;
            this.TimeCounterLabel.Location = new System.Drawing.Point(323, 298);
            this.TimeCounterLabel.Margin = new System.Windows.Forms.Padding(176, 192, 176, 192);
            this.TimeCounterLabel.Name = "TimeCounterLabel";
            this.TimeCounterLabel.Size = new System.Drawing.Size(268, 38);
            this.TimeCounterLabel.TabIndex = 1;
            this.TimeCounterLabel.Text = "Loading Times...";
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
            this.RedeemKeyTab.Margin = new System.Windows.Forms.Padding(96);
            this.RedeemKeyTab.Name = "RedeemKeyTab";
            this.RedeemKeyTab.PageEnabled = false;
            this.RedeemKeyTab.Size = new System.Drawing.Size(928, 628);
            this.RedeemKeyTab.Text = "Redeem Key";
            // 
            // hyphen3
            // 
            this.hyphen3.AutoSize = true;
            this.hyphen3.Location = new System.Drawing.Point(600, 300);
            this.hyphen3.Margin = new System.Windows.Forms.Padding(96, 0, 96, 0);
            this.hyphen3.Name = "hyphen3";
            this.hyphen3.Size = new System.Drawing.Size(20, 25);
            this.hyphen3.TabIndex = 18;
            this.hyphen3.Text = "-";
            // 
            // hyphen2
            // 
            this.hyphen2.AutoSize = true;
            this.hyphen2.Location = new System.Drawing.Point(461, 300);
            this.hyphen2.Margin = new System.Windows.Forms.Padding(96, 0, 96, 0);
            this.hyphen2.Name = "hyphen2";
            this.hyphen2.Size = new System.Drawing.Size(20, 25);
            this.hyphen2.TabIndex = 17;
            this.hyphen2.Text = "-";
            // 
            // hyphen1
            // 
            this.hyphen1.AutoSize = true;
            this.hyphen1.Location = new System.Drawing.Point(322, 300);
            this.hyphen1.Margin = new System.Windows.Forms.Padding(96, 0, 96, 0);
            this.hyphen1.Name = "hyphen1";
            this.hyphen1.Size = new System.Drawing.Size(20, 25);
            this.hyphen1.TabIndex = 16;
            this.hyphen1.Text = "-";
            // 
            // RedeemKeyTextbox4
            // 
            this.RedeemKeyTextbox4.Location = new System.Drawing.Point(630, 293);
            this.RedeemKeyTextbox4.Margin = new System.Windows.Forms.Padding(352, 384, 352, 384);
            this.RedeemKeyTextbox4.Name = "RedeemKeyTextbox4";
            this.RedeemKeyTextbox4.Properties.MaxLength = 5;
            this.RedeemKeyTextbox4.Size = new System.Drawing.Size(99, 40);
            this.RedeemKeyTextbox4.TabIndex = 15;
            // 
            // RedeemKeyTextbox3
            // 
            this.RedeemKeyTextbox3.Location = new System.Drawing.Point(491, 294);
            this.RedeemKeyTextbox3.Margin = new System.Windows.Forms.Padding(352, 384, 352, 384);
            this.RedeemKeyTextbox3.Name = "RedeemKeyTextbox3";
            this.RedeemKeyTextbox3.Properties.MaxLength = 5;
            this.RedeemKeyTextbox3.Size = new System.Drawing.Size(99, 40);
            this.RedeemKeyTextbox3.TabIndex = 14;
            // 
            // RedeemKeyTextbox2
            // 
            this.RedeemKeyTextbox2.Location = new System.Drawing.Point(352, 293);
            this.RedeemKeyTextbox2.Margin = new System.Windows.Forms.Padding(352, 384, 352, 384);
            this.RedeemKeyTextbox2.Name = "RedeemKeyTextbox2";
            this.RedeemKeyTextbox2.Properties.MaxLength = 5;
            this.RedeemKeyTextbox2.Size = new System.Drawing.Size(99, 40);
            this.RedeemKeyTextbox2.TabIndex = 13;
            // 
            // RedeemKeyLabel
            // 
            this.RedeemKeyLabel.AutoSize = true;
            this.RedeemKeyLabel.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedeemKeyLabel.ForeColor = System.Drawing.Color.Gray;
            this.RedeemKeyLabel.Location = new System.Drawing.Point(231, 131);
            this.RedeemKeyLabel.Margin = new System.Windows.Forms.Padding(96, 0, 96, 0);
            this.RedeemKeyLabel.Name = "RedeemKeyLabel";
            this.RedeemKeyLabel.Size = new System.Drawing.Size(481, 78);
            this.RedeemKeyLabel.TabIndex = 12;
            this.RedeemKeyLabel.Text = "Redeem Key";
            // 
            // RedeemKeyButton
            // 
            this.RedeemKeyButton.Location = new System.Drawing.Point(305, 399);
            this.RedeemKeyButton.Margin = new System.Windows.Forms.Padding(1424, 1536, 1424, 1536);
            this.RedeemKeyButton.Name = "RedeemKeyButton";
            this.RedeemKeyButton.Size = new System.Drawing.Size(332, 85);
            this.RedeemKeyButton.TabIndex = 7;
            this.RedeemKeyButton.Text = "Redeem";
            this.RedeemKeyButton.Click += new System.EventHandler(this.RedeemKeyButton_Click);
            // 
            // RedeemKeyTextbox1
            // 
            this.RedeemKeyTextbox1.Location = new System.Drawing.Point(213, 293);
            this.RedeemKeyTextbox1.Margin = new System.Windows.Forms.Padding(176, 192, 176, 192);
            this.RedeemKeyTextbox1.Name = "RedeemKeyTextbox1";
            this.RedeemKeyTextbox1.Properties.MaxLength = 5;
            this.RedeemKeyTextbox1.Size = new System.Drawing.Size(99, 40);
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
            this.LoginTab.Margin = new System.Windows.Forms.Padding(96);
            this.LoginTab.Name = "LoginTab";
            this.LoginTab.Size = new System.Drawing.Size(928, 628);
            this.LoginTab.Text = "Login";
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginLabel.ForeColor = System.Drawing.Color.Gray;
            this.LoginLabel.Location = new System.Drawing.Point(352, 131);
            this.LoginLabel.Margin = new System.Windows.Forms.Padding(96, 0, 96, 0);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(231, 78);
            this.LoginLabel.TabIndex = 11;
            this.LoginLabel.Text = "Login";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(102, 319);
            this.PasswordLabel.Margin = new System.Windows.Forms.Padding(96, 0, 96, 0);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(107, 25);
            this.PasswordLabel.TabIndex = 10;
            this.PasswordLabel.Text = "Password:";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(96, 268);
            this.UsernameLabel.Margin = new System.Windows.Forms.Padding(96, 0, 96, 0);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(113, 25);
            this.UsernameLabel.TabIndex = 9;
            this.UsernameLabel.Text = "Username:";
            // 
            // UsernameTextbox
            // 
            this.UsernameTextbox.Location = new System.Drawing.Point(218, 261);
            this.UsernameTextbox.Margin = new System.Windows.Forms.Padding(704, 768, 704, 768);
            this.UsernameTextbox.Name = "UsernameTextbox";
            this.UsernameTextbox.Size = new System.Drawing.Size(497, 40);
            this.UsernameTextbox.TabIndex = 8;
            // 
            // PasswordTextbox
            // 
            this.PasswordTextbox.Location = new System.Drawing.Point(218, 313);
            this.PasswordTextbox.Margin = new System.Windows.Forms.Padding(704, 768, 704, 768);
            this.PasswordTextbox.Name = "PasswordTextbox";
            this.PasswordTextbox.Properties.PasswordChar = '*';
            this.PasswordTextbox.Properties.UseSystemPasswordChar = true;
            this.PasswordTextbox.Size = new System.Drawing.Size(497, 40);
            this.PasswordTextbox.TabIndex = 7;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(301, 399);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(704, 768, 704, 768);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(332, 85);
            this.LoginButton.TabIndex = 6;
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
            this.RegisterTab.Margin = new System.Windows.Forms.Padding(48);
            this.RegisterTab.Name = "RegisterTab";
            this.RegisterTab.Size = new System.Drawing.Size(928, 628);
            this.RegisterTab.Text = "Register";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(139, 233);
            this.label4.Margin = new System.Windows.Forms.Padding(96, 0, 96, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 25);
            this.label4.TabIndex = 19;
            this.label4.Text = "Email:";
            // 
            // RegisterEmailTextbox
            // 
            this.RegisterEmailTextbox.Location = new System.Drawing.Point(219, 226);
            this.RegisterEmailTextbox.Margin = new System.Windows.Forms.Padding(704, 768, 704, 768);
            this.RegisterEmailTextbox.Name = "RegisterEmailTextbox";
            this.RegisterEmailTextbox.Size = new System.Drawing.Size(497, 40);
            this.RegisterEmailTextbox.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(300, 131);
            this.label1.Margin = new System.Windows.Forms.Padding(96, 0, 96, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 78);
            this.label1.TabIndex = 17;
            this.label1.Text = "Register";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 338);
            this.label2.Margin = new System.Windows.Forms.Padding(96, 0, 96, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 285);
            this.label3.Margin = new System.Windows.Forms.Padding(96, 0, 96, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 25);
            this.label3.TabIndex = 15;
            this.label3.Text = "Username:";
            // 
            // RegisterUsernameTextbox
            // 
            this.RegisterUsernameTextbox.Location = new System.Drawing.Point(219, 279);
            this.RegisterUsernameTextbox.Margin = new System.Windows.Forms.Padding(704, 768, 704, 768);
            this.RegisterUsernameTextbox.Name = "RegisterUsernameTextbox";
            this.RegisterUsernameTextbox.Size = new System.Drawing.Size(497, 40);
            this.RegisterUsernameTextbox.TabIndex = 14;
            // 
            // RegisterPasswordTextbox
            // 
            this.RegisterPasswordTextbox.Location = new System.Drawing.Point(219, 332);
            this.RegisterPasswordTextbox.Margin = new System.Windows.Forms.Padding(704, 768, 704, 768);
            this.RegisterPasswordTextbox.Name = "RegisterPasswordTextbox";
            this.RegisterPasswordTextbox.Properties.PasswordChar = '*';
            this.RegisterPasswordTextbox.Properties.UseSystemPasswordChar = true;
            this.RegisterPasswordTextbox.Size = new System.Drawing.Size(497, 40);
            this.RegisterPasswordTextbox.TabIndex = 13;
            // 
            // RegisterButton
            // 
            this.RegisterButton.Location = new System.Drawing.Point(301, 399);
            this.RegisterButton.Margin = new System.Windows.Forms.Padding(704, 768, 704, 768);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(332, 85);
            this.RegisterButton.TabIndex = 12;
            this.RegisterButton.Text = "Register";
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // MainTab
            // 
            this.MainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTab.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.MainTab.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.MainTab.Location = new System.Drawing.Point(0, 0);
            this.MainTab.Margin = new System.Windows.Forms.Padding(176, 192, 176, 192);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedTabPage = this.LoginTab;
            this.MainTab.Size = new System.Drawing.Size(1074, 632);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 632);
            this.Controls.Add(this.MainTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.SvgImage = global::NetClient.Properties.Resources.charttype_radarline;
            this.Name = "MainForm";
            this.Text = "Cheat Client";
            this.GameCheatTab.ResumeLayout(false);
            this.TimeTab.ResumeLayout(false);
            this.TimeTab.PerformLayout();
            this.RedeemKeyTab.ResumeLayout(false);
            this.RedeemKeyTab.PerformLayout();
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
        private System.Windows.Forms.Label TimeLabel;
        private DevExpress.XtraEditors.LabelControl EndDateLabel;
        private DevExpress.XtraEditors.LabelControl TimeCounterLabel;
        private DevExpress.XtraTab.XtraTabPage RedeemKeyTab;
        private System.Windows.Forms.Label hyphen3;
        private System.Windows.Forms.Label hyphen2;
        private System.Windows.Forms.Label hyphen1;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox4;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox3;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox2;
        private System.Windows.Forms.Label RedeemKeyLabel;
        private DevExpress.XtraEditors.SimpleButton RedeemKeyButton;
        private DevExpress.XtraEditors.TextEdit RedeemKeyTextbox1;
        private DevExpress.XtraTab.XtraTabPage LoginTab;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private DevExpress.XtraEditors.TextEdit UsernameTextbox;
        private DevExpress.XtraEditors.TextEdit PasswordTextbox;
        private DevExpress.XtraEditors.SimpleButton LoginButton;
        private DevExpress.XtraTab.XtraTabPage RegisterTab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit RegisterUsernameTextbox;
        private DevExpress.XtraEditors.TextEdit RegisterPasswordTextbox;
        private DevExpress.XtraEditors.SimpleButton RegisterButton;
        private DevExpress.XtraTab.XtraTabControl MainTab;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit RegisterEmailTextbox;
        private DevExpress.XtraEditors.TileControl CheatTiles;
        private DevExpress.XtraEditors.TileGroup CheatGroup;
    }
}

