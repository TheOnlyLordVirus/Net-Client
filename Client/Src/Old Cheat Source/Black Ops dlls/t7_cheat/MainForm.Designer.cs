﻿
namespace t7_cheat
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
            this.Page_ClientList = new DevExpress.XtraTab.XtraTabPage();
            this.Btn_DumpWeaponsForMap = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_DumpWeapons = new DevExpress.XtraEditors.SimpleButton();
            this.WeaponLabel = new DevExpress.XtraEditors.LabelControl();
            this.Btn_LoadPosition = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SavePosition = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ThirdpersonOFF = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_ThirdpersonON = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Take = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Give = new DevExpress.XtraEditors.SimpleButton();
            this.Label_PlayerWeapons = new DevExpress.XtraEditors.LabelControl();
            this.Text_Weapon = new DevExpress.XtraEditors.TextEdit();
            this.Btn_TakeAllPerks = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_GiveAllPerks = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_UnlimitedAmmoOFF = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_UnlimitedAmmoON = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_RapidFireOFF = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_RapidFireON = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SmallCrosshairOFF = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SmallCrosshairON = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_NoclipOFF = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_NoclipON = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_GodmodeOFF = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_GodmodeON = new DevExpress.XtraEditors.SimpleButton();
            this.Text_Score = new DevExpress.XtraEditors.TextEdit();
            this.Btn_RemoveScore = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_AddScore = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SelectPlayer4 = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SelectPlayer3 = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SelectPlayer2 = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_SelectPlayer1 = new DevExpress.XtraEditors.SimpleButton();
            this.Label_SelectedPlayer = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.clientButton = new DevExpress.XtraEditors.SimpleButton();
            this.clientInput = new DevExpress.XtraEditors.TextEdit();
            this.Page_ClientList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Text_Weapon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Text_Score.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientInput.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Page_ClientList
            // 
            this.Page_ClientList.Controls.Add(this.clientInput);
            this.Page_ClientList.Controls.Add(this.clientButton);
            this.Page_ClientList.Controls.Add(this.Btn_DumpWeaponsForMap);
            this.Page_ClientList.Controls.Add(this.Btn_DumpWeapons);
            this.Page_ClientList.Controls.Add(this.WeaponLabel);
            this.Page_ClientList.Controls.Add(this.Btn_LoadPosition);
            this.Page_ClientList.Controls.Add(this.Btn_SavePosition);
            this.Page_ClientList.Controls.Add(this.Btn_ThirdpersonOFF);
            this.Page_ClientList.Controls.Add(this.Btn_ThirdpersonON);
            this.Page_ClientList.Controls.Add(this.Btn_Take);
            this.Page_ClientList.Controls.Add(this.Btn_Give);
            this.Page_ClientList.Controls.Add(this.Label_PlayerWeapons);
            this.Page_ClientList.Controls.Add(this.Text_Weapon);
            this.Page_ClientList.Controls.Add(this.Btn_TakeAllPerks);
            this.Page_ClientList.Controls.Add(this.Btn_GiveAllPerks);
            this.Page_ClientList.Controls.Add(this.Btn_UnlimitedAmmoOFF);
            this.Page_ClientList.Controls.Add(this.Btn_UnlimitedAmmoON);
            this.Page_ClientList.Controls.Add(this.Btn_RapidFireOFF);
            this.Page_ClientList.Controls.Add(this.Btn_RapidFireON);
            this.Page_ClientList.Controls.Add(this.Btn_SmallCrosshairOFF);
            this.Page_ClientList.Controls.Add(this.Btn_SmallCrosshairON);
            this.Page_ClientList.Controls.Add(this.Btn_NoclipOFF);
            this.Page_ClientList.Controls.Add(this.Btn_NoclipON);
            this.Page_ClientList.Controls.Add(this.Btn_GodmodeOFF);
            this.Page_ClientList.Controls.Add(this.Btn_GodmodeON);
            this.Page_ClientList.Controls.Add(this.Text_Score);
            this.Page_ClientList.Controls.Add(this.Btn_RemoveScore);
            this.Page_ClientList.Controls.Add(this.Btn_AddScore);
            this.Page_ClientList.Controls.Add(this.Btn_SelectPlayer4);
            this.Page_ClientList.Controls.Add(this.Btn_SelectPlayer3);
            this.Page_ClientList.Controls.Add(this.Btn_SelectPlayer2);
            this.Page_ClientList.Controls.Add(this.Btn_SelectPlayer1);
            this.Page_ClientList.Controls.Add(this.Label_SelectedPlayer);
            this.Page_ClientList.Margin = new System.Windows.Forms.Padding(6);
            this.Page_ClientList.Name = "Page_ClientList";
            this.Page_ClientList.Size = new System.Drawing.Size(1471, 861);
            this.Page_ClientList.Text = "Client List";
            // 
            // Btn_DumpWeaponsForMap
            // 
            this.Btn_DumpWeaponsForMap.Location = new System.Drawing.Point(324, 760);
            this.Btn_DumpWeaponsForMap.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_DumpWeaponsForMap.Name = "Btn_DumpWeaponsForMap";
            this.Btn_DumpWeaponsForMap.Size = new System.Drawing.Size(282, 77);
            this.Btn_DumpWeaponsForMap.TabIndex = 69;
            this.Btn_DumpWeaponsForMap.Text = "Dump Weapons For Map";
            this.Btn_DumpWeaponsForMap.Click += new System.EventHandler(this.Btn_DumpWeaponsForMap_Click);
            // 
            // Btn_DumpWeapons
            // 
            this.Btn_DumpWeapons.Location = new System.Drawing.Point(30, 760);
            this.Btn_DumpWeapons.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_DumpWeapons.Name = "Btn_DumpWeapons";
            this.Btn_DumpWeapons.Size = new System.Drawing.Size(282, 77);
            this.Btn_DumpWeapons.TabIndex = 68;
            this.Btn_DumpWeapons.Text = "Get Current Weapon";
            this.Btn_DumpWeapons.Click += new System.EventHandler(this.Btn_DumpWeapons_Click);
            // 
            // WeaponLabel
            // 
            this.WeaponLabel.Location = new System.Drawing.Point(250, 581);
            this.WeaponLabel.Name = "WeaponLabel";
            this.WeaponLabel.Size = new System.Drawing.Size(123, 25);
            this.WeaponLabel.TabIndex = 42;
            this.WeaponLabel.Text = "Give Weapon";
            // 
            // Btn_LoadPosition
            // 
            this.Btn_LoadPosition.Location = new System.Drawing.Point(1188, 476);
            this.Btn_LoadPosition.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_LoadPosition.Name = "Btn_LoadPosition";
            this.Btn_LoadPosition.Size = new System.Drawing.Size(260, 44);
            this.Btn_LoadPosition.TabIndex = 41;
            this.Btn_LoadPosition.Text = "Load Position";
            this.Btn_LoadPosition.Click += new System.EventHandler(this.Btn_LoadPosition_Click);
            // 
            // Btn_SavePosition
            // 
            this.Btn_SavePosition.Location = new System.Drawing.Point(916, 476);
            this.Btn_SavePosition.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_SavePosition.Name = "Btn_SavePosition";
            this.Btn_SavePosition.Size = new System.Drawing.Size(260, 44);
            this.Btn_SavePosition.TabIndex = 40;
            this.Btn_SavePosition.Text = "Save Position";
            this.Btn_SavePosition.Click += new System.EventHandler(this.Btn_SavePosition_Click);
            // 
            // Btn_ThirdpersonOFF
            // 
            this.Btn_ThirdpersonOFF.Location = new System.Drawing.Point(1188, 420);
            this.Btn_ThirdpersonOFF.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_ThirdpersonOFF.Name = "Btn_ThirdpersonOFF";
            this.Btn_ThirdpersonOFF.Size = new System.Drawing.Size(260, 44);
            this.Btn_ThirdpersonOFF.TabIndex = 39;
            this.Btn_ThirdpersonOFF.Text = "Thirdperson OFF";
            this.Btn_ThirdpersonOFF.Click += new System.EventHandler(this.Btn_ThirdpersonOFF_Click);
            // 
            // Btn_ThirdpersonON
            // 
            this.Btn_ThirdpersonON.Location = new System.Drawing.Point(916, 420);
            this.Btn_ThirdpersonON.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_ThirdpersonON.Name = "Btn_ThirdpersonON";
            this.Btn_ThirdpersonON.Size = new System.Drawing.Size(260, 44);
            this.Btn_ThirdpersonON.TabIndex = 38;
            this.Btn_ThirdpersonON.Text = "Thirdperson ON";
            this.Btn_ThirdpersonON.Click += new System.EventHandler(this.Btn_ThirdpersonON_Click);
            // 
            // Btn_Take
            // 
            this.Btn_Take.Location = new System.Drawing.Point(369, 667);
            this.Btn_Take.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_Take.Name = "Btn_Take";
            this.Btn_Take.Size = new System.Drawing.Size(128, 44);
            this.Btn_Take.TabIndex = 37;
            this.Btn_Take.Text = "Take";
            this.Btn_Take.Click += new System.EventHandler(this.Btn_Take_Click);
            // 
            // Btn_Give
            // 
            this.Btn_Give.Location = new System.Drawing.Point(141, 667);
            this.Btn_Give.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_Give.Name = "Btn_Give";
            this.Btn_Give.Size = new System.Drawing.Size(128, 44);
            this.Btn_Give.TabIndex = 36;
            this.Btn_Give.Text = "Give";
            this.Btn_Give.Click += new System.EventHandler(this.Btn_Give_Click);
            // 
            // Label_PlayerWeapons
            // 
            this.Label_PlayerWeapons.Location = new System.Drawing.Point(30, 288);
            this.Label_PlayerWeapons.Margin = new System.Windows.Forms.Padding(6);
            this.Label_PlayerWeapons.Name = "Label_PlayerWeapons";
            this.Label_PlayerWeapons.Size = new System.Drawing.Size(0, 25);
            this.Label_PlayerWeapons.TabIndex = 35;
            // 
            // Text_Weapon
            // 
            this.Text_Weapon.Location = new System.Drawing.Point(141, 615);
            this.Text_Weapon.Margin = new System.Windows.Forms.Padding(6);
            this.Text_Weapon.Name = "Text_Weapon";
            this.Text_Weapon.Size = new System.Drawing.Size(356, 40);
            this.Text_Weapon.TabIndex = 33;
            // 
            // Btn_TakeAllPerks
            // 
            this.Btn_TakeAllPerks.Location = new System.Drawing.Point(1188, 364);
            this.Btn_TakeAllPerks.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_TakeAllPerks.Name = "Btn_TakeAllPerks";
            this.Btn_TakeAllPerks.Size = new System.Drawing.Size(260, 44);
            this.Btn_TakeAllPerks.TabIndex = 32;
            this.Btn_TakeAllPerks.Text = "Take All Perks";
            this.Btn_TakeAllPerks.Click += new System.EventHandler(this.Btn_TakeAllPerks_Click);
            // 
            // Btn_GiveAllPerks
            // 
            this.Btn_GiveAllPerks.Location = new System.Drawing.Point(916, 364);
            this.Btn_GiveAllPerks.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_GiveAllPerks.Name = "Btn_GiveAllPerks";
            this.Btn_GiveAllPerks.Size = new System.Drawing.Size(260, 44);
            this.Btn_GiveAllPerks.TabIndex = 31;
            this.Btn_GiveAllPerks.Text = "Give All Perks";
            this.Btn_GiveAllPerks.Click += new System.EventHandler(this.Btn_GiveAllPerks_Click);
            // 
            // Btn_UnlimitedAmmoOFF
            // 
            this.Btn_UnlimitedAmmoOFF.Location = new System.Drawing.Point(1188, 252);
            this.Btn_UnlimitedAmmoOFF.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_UnlimitedAmmoOFF.Name = "Btn_UnlimitedAmmoOFF";
            this.Btn_UnlimitedAmmoOFF.Size = new System.Drawing.Size(260, 44);
            this.Btn_UnlimitedAmmoOFF.TabIndex = 28;
            this.Btn_UnlimitedAmmoOFF.Text = "Unlimited Ammo OFF";
            this.Btn_UnlimitedAmmoOFF.Click += new System.EventHandler(this.Btn_UnlimitedAmmoOFF_Click);
            // 
            // Btn_UnlimitedAmmoON
            // 
            this.Btn_UnlimitedAmmoON.Location = new System.Drawing.Point(916, 252);
            this.Btn_UnlimitedAmmoON.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_UnlimitedAmmoON.Name = "Btn_UnlimitedAmmoON";
            this.Btn_UnlimitedAmmoON.Size = new System.Drawing.Size(260, 44);
            this.Btn_UnlimitedAmmoON.TabIndex = 27;
            this.Btn_UnlimitedAmmoON.Text = "Unlimited Ammo ON";
            this.Btn_UnlimitedAmmoON.Click += new System.EventHandler(this.Btn_UnlimitedAmmoON_Click);
            // 
            // Btn_RapidFireOFF
            // 
            this.Btn_RapidFireOFF.Location = new System.Drawing.Point(1188, 196);
            this.Btn_RapidFireOFF.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_RapidFireOFF.Name = "Btn_RapidFireOFF";
            this.Btn_RapidFireOFF.Size = new System.Drawing.Size(260, 44);
            this.Btn_RapidFireOFF.TabIndex = 26;
            this.Btn_RapidFireOFF.Text = "Rapid Fire OFF";
            this.Btn_RapidFireOFF.Click += new System.EventHandler(this.Btn_RapidFireOFF_Click);
            // 
            // Btn_RapidFireON
            // 
            this.Btn_RapidFireON.Location = new System.Drawing.Point(916, 196);
            this.Btn_RapidFireON.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_RapidFireON.Name = "Btn_RapidFireON";
            this.Btn_RapidFireON.Size = new System.Drawing.Size(260, 44);
            this.Btn_RapidFireON.TabIndex = 25;
            this.Btn_RapidFireON.Text = "Rapid Fire ON";
            this.Btn_RapidFireON.Click += new System.EventHandler(this.Btn_RapidFireON_Click);
            // 
            // Btn_SmallCrosshairOFF
            // 
            this.Btn_SmallCrosshairOFF.Location = new System.Drawing.Point(1188, 140);
            this.Btn_SmallCrosshairOFF.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_SmallCrosshairOFF.Name = "Btn_SmallCrosshairOFF";
            this.Btn_SmallCrosshairOFF.Size = new System.Drawing.Size(260, 44);
            this.Btn_SmallCrosshairOFF.TabIndex = 24;
            this.Btn_SmallCrosshairOFF.Text = "Small Crosshair OFF";
            this.Btn_SmallCrosshairOFF.Click += new System.EventHandler(this.Btn_SmallCrosshairOFF_Click);
            // 
            // Btn_SmallCrosshairON
            // 
            this.Btn_SmallCrosshairON.Location = new System.Drawing.Point(916, 140);
            this.Btn_SmallCrosshairON.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_SmallCrosshairON.Name = "Btn_SmallCrosshairON";
            this.Btn_SmallCrosshairON.Size = new System.Drawing.Size(260, 44);
            this.Btn_SmallCrosshairON.TabIndex = 23;
            this.Btn_SmallCrosshairON.Text = "Small Crosshair ON";
            this.Btn_SmallCrosshairON.Click += new System.EventHandler(this.Btn_SmallCrosshairON_Click);
            // 
            // Btn_NoclipOFF
            // 
            this.Btn_NoclipOFF.Location = new System.Drawing.Point(1188, 85);
            this.Btn_NoclipOFF.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_NoclipOFF.Name = "Btn_NoclipOFF";
            this.Btn_NoclipOFF.Size = new System.Drawing.Size(260, 44);
            this.Btn_NoclipOFF.TabIndex = 22;
            this.Btn_NoclipOFF.Text = "Noclip OFF";
            this.Btn_NoclipOFF.Click += new System.EventHandler(this.Btn_NoclipOFF_Click);
            // 
            // Btn_NoclipON
            // 
            this.Btn_NoclipON.Location = new System.Drawing.Point(916, 85);
            this.Btn_NoclipON.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_NoclipON.Name = "Btn_NoclipON";
            this.Btn_NoclipON.Size = new System.Drawing.Size(260, 44);
            this.Btn_NoclipON.TabIndex = 21;
            this.Btn_NoclipON.Text = "Noclip ON";
            this.Btn_NoclipON.Click += new System.EventHandler(this.Btn_NoclipON_Click);
            // 
            // Btn_GodmodeOFF
            // 
            this.Btn_GodmodeOFF.Location = new System.Drawing.Point(1188, 29);
            this.Btn_GodmodeOFF.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_GodmodeOFF.Name = "Btn_GodmodeOFF";
            this.Btn_GodmodeOFF.Size = new System.Drawing.Size(260, 44);
            this.Btn_GodmodeOFF.TabIndex = 20;
            this.Btn_GodmodeOFF.Text = "Godmode OFF";
            this.Btn_GodmodeOFF.Click += new System.EventHandler(this.Btn_GodmodeOFF_Click);
            // 
            // Btn_GodmodeON
            // 
            this.Btn_GodmodeON.Location = new System.Drawing.Point(916, 29);
            this.Btn_GodmodeON.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_GodmodeON.Name = "Btn_GodmodeON";
            this.Btn_GodmodeON.Size = new System.Drawing.Size(260, 44);
            this.Btn_GodmodeON.TabIndex = 19;
            this.Btn_GodmodeON.Text = "Godmode ON";
            this.Btn_GodmodeON.Click += new System.EventHandler(this.Btn_GodmodeON_Click);
            // 
            // Text_Score
            // 
            this.Text_Score.Location = new System.Drawing.Point(1108, 312);
            this.Text_Score.Margin = new System.Windows.Forms.Padding(6);
            this.Text_Score.Name = "Text_Score";
            this.Text_Score.Size = new System.Drawing.Size(148, 40);
            this.Text_Score.TabIndex = 18;
            // 
            // Btn_RemoveScore
            // 
            this.Btn_RemoveScore.Location = new System.Drawing.Point(916, 308);
            this.Btn_RemoveScore.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_RemoveScore.Name = "Btn_RemoveScore";
            this.Btn_RemoveScore.Size = new System.Drawing.Size(180, 44);
            this.Btn_RemoveScore.TabIndex = 17;
            this.Btn_RemoveScore.Text = "Remove Score";
            this.Btn_RemoveScore.Click += new System.EventHandler(this.Btn_RemoveScore_Click);
            // 
            // Btn_AddScore
            // 
            this.Btn_AddScore.Location = new System.Drawing.Point(1268, 308);
            this.Btn_AddScore.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_AddScore.Name = "Btn_AddScore";
            this.Btn_AddScore.Size = new System.Drawing.Size(180, 44);
            this.Btn_AddScore.TabIndex = 16;
            this.Btn_AddScore.Text = "Add Score";
            this.Btn_AddScore.Click += new System.EventHandler(this.Btn_AddScore_Click);
            // 
            // Btn_SelectPlayer4
            // 
            this.Btn_SelectPlayer4.Location = new System.Drawing.Point(30, 196);
            this.Btn_SelectPlayer4.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_SelectPlayer4.Name = "Btn_SelectPlayer4";
            this.Btn_SelectPlayer4.Size = new System.Drawing.Size(322, 44);
            this.Btn_SelectPlayer4.TabIndex = 8;
            this.Btn_SelectPlayer4.Text = "Player 4";
            this.Btn_SelectPlayer4.Click += new System.EventHandler(this.Btn_SelectPlayer4_Click);
            // 
            // Btn_SelectPlayer3
            // 
            this.Btn_SelectPlayer3.Location = new System.Drawing.Point(30, 140);
            this.Btn_SelectPlayer3.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_SelectPlayer3.Name = "Btn_SelectPlayer3";
            this.Btn_SelectPlayer3.Size = new System.Drawing.Size(322, 44);
            this.Btn_SelectPlayer3.TabIndex = 7;
            this.Btn_SelectPlayer3.Text = "Player 3";
            this.Btn_SelectPlayer3.Click += new System.EventHandler(this.Btn_SelectPlayer3_Click);
            // 
            // Btn_SelectPlayer2
            // 
            this.Btn_SelectPlayer2.Location = new System.Drawing.Point(30, 85);
            this.Btn_SelectPlayer2.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_SelectPlayer2.Name = "Btn_SelectPlayer2";
            this.Btn_SelectPlayer2.Size = new System.Drawing.Size(322, 44);
            this.Btn_SelectPlayer2.TabIndex = 6;
            this.Btn_SelectPlayer2.Text = "Player 2";
            this.Btn_SelectPlayer2.Click += new System.EventHandler(this.Btn_SelectPlayer2_Click);
            // 
            // Btn_SelectPlayer1
            // 
            this.Btn_SelectPlayer1.Location = new System.Drawing.Point(30, 29);
            this.Btn_SelectPlayer1.Margin = new System.Windows.Forms.Padding(6);
            this.Btn_SelectPlayer1.Name = "Btn_SelectPlayer1";
            this.Btn_SelectPlayer1.Size = new System.Drawing.Size(322, 44);
            this.Btn_SelectPlayer1.TabIndex = 5;
            this.Btn_SelectPlayer1.Text = "Player 1";
            this.Btn_SelectPlayer1.Click += new System.EventHandler(this.Btn_SelectPlayer1_Click);
            // 
            // Label_SelectedPlayer
            // 
            this.Label_SelectedPlayer.Location = new System.Drawing.Point(30, 252);
            this.Label_SelectedPlayer.Margin = new System.Windows.Forms.Padding(6);
            this.Label_SelectedPlayer.Name = "Label_SelectedPlayer";
            this.Label_SelectedPlayer.Size = new System.Drawing.Size(204, 25);
            this.Label_SelectedPlayer.TabIndex = 4;
            this.Label_SelectedPlayer.Text = "Selected Player Name";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(6);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.Page_ClientList;
            this.xtraTabControl1.Size = new System.Drawing.Size(1600, 865);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.Page_ClientList});
            // 
            // clientButton
            // 
            this.clientButton.Location = new System.Drawing.Point(441, 29);
            this.clientButton.Margin = new System.Windows.Forms.Padding(6);
            this.clientButton.Name = "clientButton";
            this.clientButton.Size = new System.Drawing.Size(322, 44);
            this.clientButton.TabIndex = 70;
            this.clientButton.Text = "Debug Client";
            this.clientButton.Click += new System.EventHandler(this.clientButton_Click);
            // 
            // clientInput
            // 
            this.clientInput.Location = new System.Drawing.Point(441, 88);
            this.clientInput.Margin = new System.Windows.Forms.Padding(6);
            this.clientInput.Name = "clientInput";
            this.clientInput.Size = new System.Drawing.Size(71, 40);
            this.clientInput.TabIndex = 71;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 865);
            this.Controls.Add(this.xtraTabControl1);
            this.IconOptions.SvgImage = global::t7_cheat.Properties.Resources.charttype_radarline;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainForm";
            this.Text = "T7 ZM Tool";
            this.Page_ClientList.ResumeLayout(false);
            this.Page_ClientList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Text_Weapon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Text_Score.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clientInput.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabPage Page_ClientList;
        private DevExpress.XtraEditors.SimpleButton Btn_LoadPosition;
        private DevExpress.XtraEditors.SimpleButton Btn_SavePosition;
        private DevExpress.XtraEditors.SimpleButton Btn_ThirdpersonOFF;
        private DevExpress.XtraEditors.SimpleButton Btn_ThirdpersonON;
        private DevExpress.XtraEditors.SimpleButton Btn_Take;
        private DevExpress.XtraEditors.SimpleButton Btn_Give;
        private DevExpress.XtraEditors.LabelControl Label_PlayerWeapons;
        private DevExpress.XtraEditors.TextEdit Text_Weapon;
        private DevExpress.XtraEditors.SimpleButton Btn_TakeAllPerks;
        private DevExpress.XtraEditors.SimpleButton Btn_GiveAllPerks;
        private DevExpress.XtraEditors.SimpleButton Btn_UnlimitedAmmoOFF;
        private DevExpress.XtraEditors.SimpleButton Btn_UnlimitedAmmoON;
        private DevExpress.XtraEditors.SimpleButton Btn_RapidFireOFF;
        private DevExpress.XtraEditors.SimpleButton Btn_RapidFireON;
        private DevExpress.XtraEditors.SimpleButton Btn_SmallCrosshairOFF;
        private DevExpress.XtraEditors.SimpleButton Btn_SmallCrosshairON;
        private DevExpress.XtraEditors.SimpleButton Btn_NoclipOFF;
        private DevExpress.XtraEditors.SimpleButton Btn_NoclipON;
        private DevExpress.XtraEditors.SimpleButton Btn_GodmodeOFF;
        private DevExpress.XtraEditors.SimpleButton Btn_GodmodeON;
        private DevExpress.XtraEditors.TextEdit Text_Score;
        private DevExpress.XtraEditors.SimpleButton Btn_RemoveScore;
        private DevExpress.XtraEditors.SimpleButton Btn_AddScore;
        private DevExpress.XtraEditors.SimpleButton Btn_SelectPlayer4;
        private DevExpress.XtraEditors.SimpleButton Btn_SelectPlayer3;
        private DevExpress.XtraEditors.SimpleButton Btn_SelectPlayer2;
        private DevExpress.XtraEditors.SimpleButton Btn_SelectPlayer1;
        private DevExpress.XtraEditors.LabelControl Label_SelectedPlayer;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraEditors.LabelControl WeaponLabel;
        private DevExpress.XtraEditors.SimpleButton Btn_DumpWeaponsForMap;
        private DevExpress.XtraEditors.SimpleButton Btn_DumpWeapons;
        private DevExpress.XtraEditors.TextEdit clientInput;
        private DevExpress.XtraEditors.SimpleButton clientButton;
    }
}