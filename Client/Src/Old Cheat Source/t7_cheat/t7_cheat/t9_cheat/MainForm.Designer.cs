
namespace t9_cheat
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.Page_ClientList = new DevExpress.XtraTab.XtraTabPage();
            this.Btn_Take = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Give = new DevExpress.XtraEditors.SimpleButton();
            this.Text_Weapon = new DevExpress.XtraEditors.TextEdit();
            this.Btn_Scan = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SelectPlayer4 = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SelectPlayer3 = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SelectPlayer2 = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SelectPlayer1 = new DevExpress.XtraEditors.SimpleButton();
            this.Label_SelectedPlayer = new DevExpress.XtraEditors.LabelControl();
            this.Btn_LoadPosition = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SavePosition = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ThirdpersonOFF = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ThirdpersonON = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_TakeAllPerks = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_GiveAllPerks = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ForgeModeOFF = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ForgeModeON = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_UnlimitedAmmoOFF = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_UnlimitedAmmoON = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_RapidFireOFF = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_RapidFireON = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_NoclipOFF = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_NoclipON = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_GodmodeOFF = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_GodmodeON = new DevExpress.XtraEditors.SimpleButton();
            this.Text_Score = new DevExpress.XtraEditors.TextEdit();
            this.Btn_RemoveScore = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_AddScore = new DevExpress.XtraEditors.SimpleButton();
            this.Page_Lobby = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.Page_ClientList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Text_Weapon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Text_Score.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.Page_ClientList;
            this.xtraTabControl1.Size = new System.Drawing.Size(1600, 865);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.Page_ClientList,
            this.Page_Lobby});
            // 
            // Page_ClientList
            // 
            this.Page_ClientList.Controls.Add(this.Btn_Take);
            this.Page_ClientList.Controls.Add(this.Btn_Give);
            this.Page_ClientList.Controls.Add(this.Text_Weapon);
            this.Page_ClientList.Controls.Add(this.Btn_Scan);
            this.Page_ClientList.Controls.Add(this.Btn_SelectPlayer4);
            this.Page_ClientList.Controls.Add(this.Btn_SelectPlayer3);
            this.Page_ClientList.Controls.Add(this.Btn_SelectPlayer2);
            this.Page_ClientList.Controls.Add(this.Btn_SelectPlayer1);
            this.Page_ClientList.Controls.Add(this.Label_SelectedPlayer);
            this.Page_ClientList.Controls.Add(this.Btn_LoadPosition);
            this.Page_ClientList.Controls.Add(this.Btn_SavePosition);
            this.Page_ClientList.Controls.Add(this.Btn_ThirdpersonOFF);
            this.Page_ClientList.Controls.Add(this.Btn_ThirdpersonON);
            this.Page_ClientList.Controls.Add(this.Btn_TakeAllPerks);
            this.Page_ClientList.Controls.Add(this.Btn_GiveAllPerks);
            this.Page_ClientList.Controls.Add(this.Btn_ForgeModeOFF);
            this.Page_ClientList.Controls.Add(this.Btn_ForgeModeON);
            this.Page_ClientList.Controls.Add(this.Btn_UnlimitedAmmoOFF);
            this.Page_ClientList.Controls.Add(this.Btn_UnlimitedAmmoON);
            this.Page_ClientList.Controls.Add(this.Btn_RapidFireOFF);
            this.Page_ClientList.Controls.Add(this.Btn_RapidFireON);
            this.Page_ClientList.Controls.Add(this.Btn_NoclipOFF);
            this.Page_ClientList.Controls.Add(this.Btn_NoclipON);
            this.Page_ClientList.Controls.Add(this.Btn_GodmodeOFF);
            this.Page_ClientList.Controls.Add(this.Btn_GodmodeON);
            this.Page_ClientList.Controls.Add(this.Text_Score);
            this.Page_ClientList.Controls.Add(this.Btn_RemoveScore);
            this.Page_ClientList.Controls.Add(this.Btn_AddScore);
            this.Page_ClientList.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Page_ClientList.Name = "Page_ClientList";
            this.Page_ClientList.Size = new System.Drawing.Size(1471, 861);
            this.Page_ClientList.Text = "Client List";
            // 
            // Btn_Take
            // 
            this.Btn_Take.Location = new System.Drawing.Point(732, 77);
            this.Btn_Take.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_Take.Name = "Btn_Take";
            this.Btn_Take.Size = new System.Drawing.Size(128, 44);
            this.Btn_Take.TabIndex = 93;
            this.Btn_Take.Text = "Take";
            this.Btn_Take.Click += new System.EventHandler(this.Btn_Take_Click);
            // 
            // Btn_Give
            // 
            this.Btn_Give.Location = new System.Drawing.Point(504, 77);
            this.Btn_Give.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_Give.Name = "Btn_Give";
            this.Btn_Give.Size = new System.Drawing.Size(128, 44);
            this.Btn_Give.TabIndex = 92;
            this.Btn_Give.Text = "Give";
            this.Btn_Give.Click += new System.EventHandler(this.Btn_Give_Click);
            // 
            // Text_Weapon
            // 
            this.Text_Weapon.Location = new System.Drawing.Point(504, 25);
            this.Text_Weapon.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Text_Weapon.Name = "Text_Weapon";
            this.Text_Weapon.Size = new System.Drawing.Size(356, 40);
            this.Text_Weapon.TabIndex = 91;
            // 
            // Btn_Scan
            // 
            this.Btn_Scan.Location = new System.Drawing.Point(22, 771);
            this.Btn_Scan.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_Scan.Name = "Btn_Scan";
            this.Btn_Scan.Size = new System.Drawing.Size(240, 69);
            this.Btn_Scan.TabIndex = 90;
            this.Btn_Scan.Text = "Scan";
            this.Btn_Scan.Click += new System.EventHandler(this.Btn_Scan_Click);
            // 
            // Btn_SelectPlayer4
            // 
            this.Btn_SelectPlayer4.Location = new System.Drawing.Point(22, 188);
            this.Btn_SelectPlayer4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_SelectPlayer4.Name = "Btn_SelectPlayer4";
            this.Btn_SelectPlayer4.Size = new System.Drawing.Size(322, 44);
            this.Btn_SelectPlayer4.TabIndex = 89;
            this.Btn_SelectPlayer4.Text = "Player 4";
            // 
            // Btn_SelectPlayer3
            // 
            this.Btn_SelectPlayer3.Location = new System.Drawing.Point(22, 133);
            this.Btn_SelectPlayer3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_SelectPlayer3.Name = "Btn_SelectPlayer3";
            this.Btn_SelectPlayer3.Size = new System.Drawing.Size(322, 44);
            this.Btn_SelectPlayer3.TabIndex = 88;
            this.Btn_SelectPlayer3.Text = "Player 3";
            // 
            // Btn_SelectPlayer2
            // 
            this.Btn_SelectPlayer2.Location = new System.Drawing.Point(22, 77);
            this.Btn_SelectPlayer2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_SelectPlayer2.Name = "Btn_SelectPlayer2";
            this.Btn_SelectPlayer2.Size = new System.Drawing.Size(322, 44);
            this.Btn_SelectPlayer2.TabIndex = 87;
            this.Btn_SelectPlayer2.Text = "Player 2";
            // 
            // Btn_SelectPlayer1
            // 
            this.Btn_SelectPlayer1.Location = new System.Drawing.Point(22, 21);
            this.Btn_SelectPlayer1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_SelectPlayer1.Name = "Btn_SelectPlayer1";
            this.Btn_SelectPlayer1.Size = new System.Drawing.Size(322, 44);
            this.Btn_SelectPlayer1.TabIndex = 86;
            this.Btn_SelectPlayer1.Text = "Player 1";
            // 
            // Label_SelectedPlayer
            // 
            this.Label_SelectedPlayer.Location = new System.Drawing.Point(22, 244);
            this.Label_SelectedPlayer.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Label_SelectedPlayer.Name = "Label_SelectedPlayer";
            this.Label_SelectedPlayer.Size = new System.Drawing.Size(204, 25);
            this.Label_SelectedPlayer.TabIndex = 85;
            this.Label_SelectedPlayer.Text = "Selected Player Name";
            // 
            // Btn_LoadPosition
            // 
            this.Btn_LoadPosition.Location = new System.Drawing.Point(1144, 467);
            this.Btn_LoadPosition.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_LoadPosition.Name = "Btn_LoadPosition";
            this.Btn_LoadPosition.Size = new System.Drawing.Size(260, 44);
            this.Btn_LoadPosition.TabIndex = 84;
            this.Btn_LoadPosition.Text = "Load Position";
            this.Btn_LoadPosition.Click += new System.EventHandler(this.Btn_LoadPosition_Click);
            // 
            // Btn_SavePosition
            // 
            this.Btn_SavePosition.Location = new System.Drawing.Point(872, 467);
            this.Btn_SavePosition.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_SavePosition.Name = "Btn_SavePosition";
            this.Btn_SavePosition.Size = new System.Drawing.Size(260, 44);
            this.Btn_SavePosition.TabIndex = 83;
            this.Btn_SavePosition.Text = "Save Position";
            this.Btn_SavePosition.Click += new System.EventHandler(this.Btn_SavePosition_Click);
            // 
            // Btn_ThirdpersonOFF
            // 
            this.Btn_ThirdpersonOFF.Location = new System.Drawing.Point(1144, 412);
            this.Btn_ThirdpersonOFF.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_ThirdpersonOFF.Name = "Btn_ThirdpersonOFF";
            this.Btn_ThirdpersonOFF.Size = new System.Drawing.Size(260, 44);
            this.Btn_ThirdpersonOFF.TabIndex = 82;
            this.Btn_ThirdpersonOFF.Text = "Thirdperson OFF";
            this.Btn_ThirdpersonOFF.Click += new System.EventHandler(this.Btn_ThirdpersonOFF_Click);
            // 
            // Btn_ThirdpersonON
            // 
            this.Btn_ThirdpersonON.Location = new System.Drawing.Point(872, 412);
            this.Btn_ThirdpersonON.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_ThirdpersonON.Name = "Btn_ThirdpersonON";
            this.Btn_ThirdpersonON.Size = new System.Drawing.Size(260, 44);
            this.Btn_ThirdpersonON.TabIndex = 81;
            this.Btn_ThirdpersonON.Text = "Thirdperson ON";
            this.Btn_ThirdpersonON.Click += new System.EventHandler(this.Btn_ThirdpersonON_Click);
            // 
            // Btn_TakeAllPerks
            // 
            this.Btn_TakeAllPerks.Location = new System.Drawing.Point(1144, 356);
            this.Btn_TakeAllPerks.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_TakeAllPerks.Name = "Btn_TakeAllPerks";
            this.Btn_TakeAllPerks.Size = new System.Drawing.Size(260, 44);
            this.Btn_TakeAllPerks.TabIndex = 80;
            this.Btn_TakeAllPerks.Text = "Take All Perks";
            this.Btn_TakeAllPerks.Click += new System.EventHandler(this.Btn_TakeAllPerks_Click);
            // 
            // Btn_GiveAllPerks
            // 
            this.Btn_GiveAllPerks.Location = new System.Drawing.Point(872, 356);
            this.Btn_GiveAllPerks.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_GiveAllPerks.Name = "Btn_GiveAllPerks";
            this.Btn_GiveAllPerks.Size = new System.Drawing.Size(260, 44);
            this.Btn_GiveAllPerks.TabIndex = 79;
            this.Btn_GiveAllPerks.Text = "Give All Perks";
            this.Btn_GiveAllPerks.Click += new System.EventHandler(this.Btn_GiveAllPerks_Click);
            // 
            // Btn_ForgeModeOFF
            // 
            this.Btn_ForgeModeOFF.Location = new System.Drawing.Point(1144, 300);
            this.Btn_ForgeModeOFF.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_ForgeModeOFF.Name = "Btn_ForgeModeOFF";
            this.Btn_ForgeModeOFF.Size = new System.Drawing.Size(260, 44);
            this.Btn_ForgeModeOFF.TabIndex = 78;
            this.Btn_ForgeModeOFF.Text = "Forge Mode OFF";
            // 
            // Btn_ForgeModeON
            // 
            this.Btn_ForgeModeON.Location = new System.Drawing.Point(872, 300);
            this.Btn_ForgeModeON.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_ForgeModeON.Name = "Btn_ForgeModeON";
            this.Btn_ForgeModeON.Size = new System.Drawing.Size(260, 44);
            this.Btn_ForgeModeON.TabIndex = 77;
            this.Btn_ForgeModeON.Text = "Forge Mode ON";
            // 
            // Btn_UnlimitedAmmoOFF
            // 
            this.Btn_UnlimitedAmmoOFF.Location = new System.Drawing.Point(1144, 188);
            this.Btn_UnlimitedAmmoOFF.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_UnlimitedAmmoOFF.Name = "Btn_UnlimitedAmmoOFF";
            this.Btn_UnlimitedAmmoOFF.Size = new System.Drawing.Size(260, 44);
            this.Btn_UnlimitedAmmoOFF.TabIndex = 76;
            this.Btn_UnlimitedAmmoOFF.Text = "Unlimited Ammo OFF";
            this.Btn_UnlimitedAmmoOFF.Click += new System.EventHandler(this.Btn_UnlimitedAmmoOFF_Click);
            // 
            // Btn_UnlimitedAmmoON
            // 
            this.Btn_UnlimitedAmmoON.Location = new System.Drawing.Point(872, 188);
            this.Btn_UnlimitedAmmoON.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_UnlimitedAmmoON.Name = "Btn_UnlimitedAmmoON";
            this.Btn_UnlimitedAmmoON.Size = new System.Drawing.Size(260, 44);
            this.Btn_UnlimitedAmmoON.TabIndex = 75;
            this.Btn_UnlimitedAmmoON.Text = "Unlimited Ammo ON";
            this.Btn_UnlimitedAmmoON.Click += new System.EventHandler(this.Btn_UnlimitedAmmoON_Click);
            // 
            // Btn_RapidFireOFF
            // 
            this.Btn_RapidFireOFF.Location = new System.Drawing.Point(1144, 133);
            this.Btn_RapidFireOFF.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_RapidFireOFF.Name = "Btn_RapidFireOFF";
            this.Btn_RapidFireOFF.Size = new System.Drawing.Size(260, 44);
            this.Btn_RapidFireOFF.TabIndex = 74;
            this.Btn_RapidFireOFF.Text = "Rapid Fire OFF";
            this.Btn_RapidFireOFF.Click += new System.EventHandler(this.Btn_RapidFireOFF_Click);
            // 
            // Btn_RapidFireON
            // 
            this.Btn_RapidFireON.Location = new System.Drawing.Point(872, 133);
            this.Btn_RapidFireON.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_RapidFireON.Name = "Btn_RapidFireON";
            this.Btn_RapidFireON.Size = new System.Drawing.Size(260, 44);
            this.Btn_RapidFireON.TabIndex = 73;
            this.Btn_RapidFireON.Text = "Rapid Fire ON";
            this.Btn_RapidFireON.Click += new System.EventHandler(this.Btn_RapidFireON_Click);
            // 
            // Btn_NoclipOFF
            // 
            this.Btn_NoclipOFF.Location = new System.Drawing.Point(1144, 77);
            this.Btn_NoclipOFF.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_NoclipOFF.Name = "Btn_NoclipOFF";
            this.Btn_NoclipOFF.Size = new System.Drawing.Size(260, 44);
            this.Btn_NoclipOFF.TabIndex = 72;
            this.Btn_NoclipOFF.Text = "Noclip OFF";
            // 
            // Btn_NoclipON
            // 
            this.Btn_NoclipON.Location = new System.Drawing.Point(872, 77);
            this.Btn_NoclipON.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_NoclipON.Name = "Btn_NoclipON";
            this.Btn_NoclipON.Size = new System.Drawing.Size(260, 44);
            this.Btn_NoclipON.TabIndex = 71;
            this.Btn_NoclipON.Text = "Noclip ON";
            // 
            // Btn_GodmodeOFF
            // 
            this.Btn_GodmodeOFF.Location = new System.Drawing.Point(1144, 21);
            this.Btn_GodmodeOFF.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_GodmodeOFF.Name = "Btn_GodmodeOFF";
            this.Btn_GodmodeOFF.Size = new System.Drawing.Size(260, 44);
            this.Btn_GodmodeOFF.TabIndex = 70;
            this.Btn_GodmodeOFF.Text = "Godmode OFF";
            this.Btn_GodmodeOFF.Click += new System.EventHandler(this.Btn_GodmodeOFF_Click);
            // 
            // Btn_GodmodeON
            // 
            this.Btn_GodmodeON.Location = new System.Drawing.Point(872, 21);
            this.Btn_GodmodeON.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_GodmodeON.Name = "Btn_GodmodeON";
            this.Btn_GodmodeON.Size = new System.Drawing.Size(260, 44);
            this.Btn_GodmodeON.TabIndex = 69;
            this.Btn_GodmodeON.Text = "Godmode ON";
            this.Btn_GodmodeON.Click += new System.EventHandler(this.Btn_GodmodeON_Click);
            // 
            // Text_Score
            // 
            this.Text_Score.Location = new System.Drawing.Point(1064, 248);
            this.Text_Score.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Text_Score.Name = "Text_Score";
            this.Text_Score.Size = new System.Drawing.Size(148, 40);
            this.Text_Score.TabIndex = 68;
            // 
            // Btn_RemoveScore
            // 
            this.Btn_RemoveScore.Location = new System.Drawing.Point(872, 244);
            this.Btn_RemoveScore.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_RemoveScore.Name = "Btn_RemoveScore";
            this.Btn_RemoveScore.Size = new System.Drawing.Size(180, 44);
            this.Btn_RemoveScore.TabIndex = 67;
            this.Btn_RemoveScore.Text = "Remove Score";
            this.Btn_RemoveScore.Click += new System.EventHandler(this.Btn_RemoveScore_Click);
            // 
            // Btn_AddScore
            // 
            this.Btn_AddScore.Location = new System.Drawing.Point(1224, 244);
            this.Btn_AddScore.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Btn_AddScore.Name = "Btn_AddScore";
            this.Btn_AddScore.Size = new System.Drawing.Size(180, 44);
            this.Btn_AddScore.TabIndex = 66;
            this.Btn_AddScore.Text = "Add Score";
            this.Btn_AddScore.Click += new System.EventHandler(this.Btn_AddScore_Click);
            // 
            // Page_Lobby
            // 
            this.Page_Lobby.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Page_Lobby.Name = "Page_Lobby";
            this.Page_Lobby.Size = new System.Drawing.Size(1471, 861);
            this.Page_Lobby.Text = "Lobby";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 865);
            this.Controls.Add(this.xtraTabControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("MainForm.IconOptions.SvgImage")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "MainForm";
            this.Text = "T9 Cheat";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.Page_ClientList.ResumeLayout(false);
            this.Page_ClientList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Text_Weapon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Text_Score.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage Page_ClientList;
        private DevExpress.XtraEditors.SimpleButton Btn_LoadPosition;
        private DevExpress.XtraEditors.SimpleButton Btn_SavePosition;
        private DevExpress.XtraEditors.SimpleButton Btn_ThirdpersonOFF;
        private DevExpress.XtraEditors.SimpleButton Btn_ThirdpersonON;
        private DevExpress.XtraEditors.SimpleButton Btn_TakeAllPerks;
        private DevExpress.XtraEditors.SimpleButton Btn_GiveAllPerks;
        private DevExpress.XtraEditors.SimpleButton Btn_ForgeModeOFF;
        private DevExpress.XtraEditors.SimpleButton Btn_ForgeModeON;
        private DevExpress.XtraEditors.SimpleButton Btn_UnlimitedAmmoOFF;
        private DevExpress.XtraEditors.SimpleButton Btn_UnlimitedAmmoON;
        private DevExpress.XtraEditors.SimpleButton Btn_RapidFireOFF;
        private DevExpress.XtraEditors.SimpleButton Btn_RapidFireON;
        private DevExpress.XtraEditors.SimpleButton Btn_NoclipOFF;
        private DevExpress.XtraEditors.SimpleButton Btn_NoclipON;
        private DevExpress.XtraEditors.SimpleButton Btn_GodmodeOFF;
        private DevExpress.XtraEditors.SimpleButton Btn_GodmodeON;
        private DevExpress.XtraEditors.TextEdit Text_Score;
        private DevExpress.XtraEditors.SimpleButton Btn_RemoveScore;
        private DevExpress.XtraEditors.SimpleButton Btn_AddScore;
        private DevExpress.XtraTab.XtraTabPage Page_Lobby;
        private DevExpress.XtraEditors.SimpleButton Btn_SelectPlayer4;
        private DevExpress.XtraEditors.SimpleButton Btn_SelectPlayer3;
        private DevExpress.XtraEditors.SimpleButton Btn_SelectPlayer2;
        private DevExpress.XtraEditors.SimpleButton Btn_SelectPlayer1;
        private DevExpress.XtraEditors.LabelControl Label_SelectedPlayer;
        private DevExpress.XtraEditors.SimpleButton Btn_Scan;
        private DevExpress.XtraEditors.SimpleButton Btn_Take;
        private DevExpress.XtraEditors.SimpleButton Btn_Give;
        private DevExpress.XtraEditors.TextEdit Text_Weapon;
    }
}

