using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraEditors;

namespace t7_cheat
{
    public partial class MainForm : XtraForm
    {
        private static Memory memory = new Memory();

        //private static gclient g_clients;
        public static WeaponDef bg_weaponDefs;

        private int CurrentPlayer;

        private bool[] brapid_fire = new bool[4];

        private bool[] bunlimited_ammo = new bool[4];

        private Vector[] saved_locations = new Vector[4];

        gclient[] g_clients = new gclient[4];

        public static string[] perknames = new string[] {     "specialty_additionalprimaryweapon",
                                                        "specialty_accuracyandflatspread",
                                                        "specialty_ammodrainsfromstockfirst",
                                                        "specialty_anteup",
                                                        "specialty_armorpiercing",
                                                        "specialty_armorvest",
                                                        "specialty_bulletaccuracy",
                                                        "specialty_bulletdamage",
                                                        "specialty_bulletflinch",
                                                        "specialty_bulletpenetration",
                                                        "specialty_combat_efficiency",
                                                        "specialty_deadshot",
                                                        "specialty_decoy",
                                                        "specialty_delayexplosive",
                                                        "specialty_detectexplosive",
                                                        "specialty_detectnearbyenemies",
                                                        "specialty_directionalfire",
                                                        "specialty_disarmexplosive",
                                                        "specialty_doubletap2",
                                                        "specialty_earnmoremomentum",
                                                        "specialty_electriccherry",
                                                        "specialty_extraammo",
                                                        "specialty_fallheight",
                                                        "specialty_fastads",
                                                        "specialty_fastequipmentuse",
                                                        "specialty_fastladderclimb",
                                                        "specialty_fastmantle",
                                                        "specialty_fastmeleerecovery",
                                                        "specialty_fastreload",
                                                        "specialty_fasttoss",
                                                        "specialty_fastweaponswitch",
                                                        "specialty_finalstand",
                                                        "specialty_fireproof",
                                                        "specialty_flakjacket",
                                                        "specialty_flashprotection",
                                                        "specialty_gpsjammer",
                                                        "specialty_grenadepulldeath",
                                                        "specialty_healthregen",
                                                        "specialty_holdbreath",
                                                        "specialty_immunecounteruav",
                                                        "specialty_immuneemp",
                                                        "specialty_immunemms",
                                                        "specialty_immunenvthermal",
                                                        "specialty_immunerangefinder",
                                                        "specialty_immunesmoke",
                                                        "specialty_immunetriggerbetty",
                                                        "specialty_immunetriggerc4",
                                                        "specialty_immunetriggershock",
                                                        "specialty_jetcharger",
                                                        "specialty_jetnoradar",
                                                        "specialty_jetpack",
                                                        "specialty_jetquiet",
                                                        "specialty_killstreak",
                                                        "specialty_longersprint",
                                                        "specialty_loudenemies",
                                                        "specialty_lowgravity",
                                                        "specialty_marksman",
                                                        "specialty_microwaveprotection",
                                                        "specialty_movefaster",
                                                        "specialty_nokillstreakreticle",
                                                        "specialty_nomotionsensor",
                                                        "specialty_noname",
                                                        "specialty_nottargetedbyairsupport",
                                                        "specialty_nottargetedbyaitank",
                                                        "specialty_nottargetedbyraps",
                                                        "specialty_nottargetedbyrobot",
                                                        "specialty_nottargetedbysentry",
                                                        "specialty_overcharge",
                                                        "specialty_phdflopper",
                                                        "specialty_pin_back",
                                                        "specialty_pistoldeath",
                                                        "specialty_playeriszombie",
                                                        "specialty_proximityprotection",
                                                        "specialty_quickrevive",
                                                        "specialty_quieter",
                                                        "specialty_rof",
                                                        "specialty_scavenger",
                                                        "specialty_sengrenjammer",
                                                        "specialty_shellshock",
                                                        "specialty_showenemyequipment",
                                                        "specialty_showenemyvehicles",
                                                        "specialty_showscorestreakicons",
                                                        "specialty_sixthsensejammer",
                                                        "specialty_spawnpingenemies",
                                                        "specialty_sprintequipment",
                                                        "specialty_sprintfire",
                                                        "specialty_sprintgrenadelethal",
                                                        "specialty_sprintgrenadetactical",
                                                        "specialty_sprintrecovery",
                                                        "specialty_sprintfirerecovery",
                                                        "specialty_stalker",
                                                        "specialty_staminup",
                                                        "specialty_stunprotection",
                                                        "specialty_teflon",
                                                        "specialty_tombstone",
                                                        "specialty_tracer",
                                                        "specialty_tracker",
                                                        "specialty_trackerjammer",
                                                        "specialty_twogrenades",
                                                        "specialty_twoprimaries",
                                                        "specialty_unlimitedsprint",
                                                        "specialty_vultureaid",
                                                        "specialty_whoswho",
                                                        "specialty_widowswine",
                                                        "specialty_locdamagecountsasheadshot" };

        public MainForm()
        {
            bg_weaponDefs = new WeaponDef(memory);
            InitializeComponent();

            for (int i = 0;i < 4;i++)
            {
                g_clients[i] = new gclient(memory);
            }

            new Task(new Action(unlimited_ammo)).Start();
        }

        private gclient GetGclient(int index)
        {
            gclient client = new gclient(memory);
            Int64 baseAddr = memory.CurrentProcess.MainModule.BaseAddress.ToInt64();
            client.clientBase = (memory.ReadPointer(baseAddr + (Int64)gclient.Addresses.gclient_a) + (index * 0x171F0));
            return client;
        }

        private void unlimited_ammo()
        {
            while (true)
            {
                if (memory.IsSafe())
                {
                    if (memory.IsProcessRunning(Program.GameName))
                    {
                        for (int i = 0;i < 4;i++)
                        {
                            g_clients[i].SetupClient(i);
                            string curWeap = g_clients[i].GetCurrentWeapon();
                            int weapSlot = g_clients[i].GetWeaponSlot(curWeap);
                            if (bunlimited_ammo[i])
                            {
                                g_clients[i].ammoInClip[weapSlot] = 100 * bg_weaponDefs[(int)(g_clients[i].currentWeapon & 0x1FF)].iClipSize;
                            }
                            if (brapid_fire[i])
                            {
                                g_clients[i].weapTime = 0;
                            }
                        }
                    }
                }
                Thread.Sleep(20);
            }
        }

        private void Btn_TestPerks_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    //MessageBox.Show($"{g_clients[0].GetCurrentWeapon()}");
                }
            }
        }

        private void Btn_SelectPlayer1_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    CurrentPlayer = 0;
                    g_clients[CurrentPlayer].SetupClient(CurrentPlayer);
                    Label_SelectedPlayer.Text = g_clients[CurrentPlayer].name;
                }
            }
        }

        private void Btn_SelectPlayer2_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    CurrentPlayer = 1;
                    g_clients[CurrentPlayer].SetupClient(CurrentPlayer);
                    Label_SelectedPlayer.Text = g_clients[CurrentPlayer].name;
                }
            }
        }

        private void Btn_SelectPlayer3_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    CurrentPlayer = 2;
                    g_clients[CurrentPlayer].SetupClient(CurrentPlayer);
                    Label_SelectedPlayer.Text = g_clients[CurrentPlayer].name;
                }
            }
        }

        private void Btn_SelectPlayer4_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    CurrentPlayer = 3;
                    g_clients[CurrentPlayer].SetupClient(CurrentPlayer);
                    Label_SelectedPlayer.Text = g_clients[CurrentPlayer].name;
                }
            }
        }

        private void Btn_AddScore_Click(object sender, EventArgs e)
        {
            if (memory.IsProcessRunning(Program.GameName))
            {
                try
                {
                    g_clients[CurrentPlayer].score += int.Parse(Text_Score.Text);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }

        private void Btn_RemoveScore_Click(object sender, EventArgs e)
        {
            if (memory.IsProcessRunning(Program.GameName))
            {
                try
                {
                    g_clients[CurrentPlayer].score -= int.Parse(Text_Score.Text);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }

        private void Btn_TestButton_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    gclient self = g_clients[CurrentPlayer];
                    string weaplist = "";
                    for (int i = 0;i < 15;i++)
                    {
                        int iWeap = (int)(self.weapons[i] & 0x1FF);
                        if (iWeap != 0)
                        {
                            weaplist += $"{bg_weaponDefs[iWeap].szInternalName}\n";
                        }
                    }
                    string curweap = self.GetCurrentWeapon();
                    int weapSlot = self.GetWeaponSlot(curweap);
                    weaplist += $"{curweap} - {weapSlot} - {self.ammoInClip[weapSlot]}";
                    Label_PlayerWeapons.Text = weaplist;
                    self.MaxAmmo();
                    //g_clients[0].TakeWeapon("sniper_powerbolt_zm");
                    //g_clients[0].GiveWeapon("lmg_heavy_zm");
                    //for (int i = 0;i < bg_weaponDefs.Length;i++)
                    //{
                    //    Console.WriteLine($"{bg_weaponDefs[i].szInternalName}");
                    //}
                }
            }
        }

        private void Btn_Give_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    int weapIndex = bg_weaponDefs.GetIndexForName(Text_Weapon.Text);
                    if (weapIndex != -1)
                    {
                        if (!g_clients[CurrentPlayer].HasWeapon(Text_Weapon.Text))
                        {
                            g_clients[CurrentPlayer].GiveWeapon(Text_Weapon.Text);
                            int weapSlot = g_clients[CurrentPlayer].GetWeaponSlot(Text_Weapon.Text);
                            g_clients[CurrentPlayer].ammoInClip[weapSlot] = 100000;
                            g_clients[CurrentPlayer].ammoNotInClip[weapSlot] = 100000;

                        }
                        else
                        {
                            XtraMessageBox.Show("Player already has that weapon!");
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Please enter a valid weapon name for this map!");
                    }
                }
            }
        }

        private void Btn_Take_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    if (g_clients[CurrentPlayer].HasWeapon(Text_Weapon.Text))
                    {
                        g_clients[CurrentPlayer].TakeWeapon(Text_Weapon.Text);
                    }
                    else
                    {
                        XtraMessageBox.Show("You do not have that weapon therefore you cannot take it away!");
                    }
                }
            }
        }

        private void Btn_GodmodeON_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    g_clients[CurrentPlayer].EnableInvulnerability();
                }
            }
        }

        private void Btn_GodmodeOFF_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    g_clients[CurrentPlayer].DisableInvulnerability();
                }
            }
        }

        private void Btn_NoclipON_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    g_clients[CurrentPlayer].EnableNoclip();
                }
            }
        }

        private void Btn_NoclipOFF_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    g_clients[CurrentPlayer].DisableNoclip();
                }
            }
        }

        private void Btn_SmallCrosshairON_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    g_clients[CurrentPlayer].SetSpreadOverride(0);
                }
            }
        }

        private void Btn_SmallCrosshairOFF_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    g_clients[CurrentPlayer].ResetSpreadOverride();
                }
            }
        }

        private void Btn_RapidFireON_Click(object sender, EventArgs e)
        {

        }

        private void Btn_RapidFireOFF_Click(object sender, EventArgs e)
        {

        }

        private void Btn_UnlimitedAmmoON_Click(object sender, EventArgs e)
        {

        }

        private void Btn_UnlimitedAmmoOFF_Click(object sender, EventArgs e)
        {

        }

        private void Btn_ForgeModeON_Click(object sender, EventArgs e)
        {

        }

        private void Btn_ForgeModeOFF_Click(object sender, EventArgs e)
        {

        }

        private void Btn_GiveAllPerks_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    for (int iPerk = 0;iPerk < perknames.Length;iPerk++)
                    {
                        g_clients[CurrentPlayer].SetPerk(perknames[iPerk]);
                    }
                }
            }
        }

        private void Btn_TakeAllPerks_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    for (int iPerk = 0; iPerk < perknames.Length; iPerk++)
                    {
                        g_clients[CurrentPlayer].UnsetPerk(perknames[iPerk]);
                    }
                }
            }
        }

        private void Btn_ThirdpersonON_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    g_clients[CurrentPlayer].bThirdPerson = 1;
                }
            }
        }

        private void Btn_ThirdpersonOFF_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    g_clients[CurrentPlayer].bThirdPerson = 0;
                }
            }
        }

        private void Btn_SavePosition_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    float[] vec = g_clients[CurrentPlayer].origin;
                    saved_locations[CurrentPlayer].x = vec[0];
                    saved_locations[CurrentPlayer].y = vec[1];
                    saved_locations[CurrentPlayer].z = vec[2];
                }
            }
        }

        private void Btn_LoadPosition_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    Vector v = saved_locations[CurrentPlayer];
                    float[] vec = new float[3] { v.x, v.y, v.z};
                    g_clients[CurrentPlayer].origin = vec;
                }
            }
        }
    }

    class Vector
    {
        public Vector(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
        public float x;
        public float y;
        public float z;
    }


}
