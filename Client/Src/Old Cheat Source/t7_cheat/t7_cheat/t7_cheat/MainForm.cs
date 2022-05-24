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
using System.IO;

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

        public MainForm()
        {
            bg_weaponDefs = new WeaponDef(memory);
            InitializeComponent();

            new Task(new Action(unlimited_ammo)).Start();
        }

        private gclient GetGclient(int index)
        {
            gclient client = new gclient(memory);
            Int64 baseAddr = memory.CurrentProcess.MainModule.BaseAddress.ToInt64();
            client.clientBase = (memory.ReadPointer(baseAddr + (Int64)gclient.Addresses.gclient_a) + (index * 0x171F0));
            return client;
        }

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


        void unlimited_ammo()
        {
            while (true)
            {
                if (memory.IsSafe())
                {
                    if (memory.IsProcessRunning(Program.GameName))
                    {
                        for (int i = 0;i < 4;i++)
                        {
                            gclient self = GetGclient(i);
                            string curWeap = self.GetCurrentWeapon();
                            int weapSlot = self.GetWeaponSlot(curWeap);
                            if (bunlimited_ammo[i])
                            {
                                self.ammoInClip[weapSlot] = bg_weaponDefs[(int)(self.currentWeapon & 0x1FF)].iClipSize;
                            }
                            if (brapid_fire[i])
                            {
                                self.weapTime = 0;
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
                    Label_SelectedPlayer.Text = GetGclient(CurrentPlayer).name;
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
                    Label_SelectedPlayer.Text = GetGclient(CurrentPlayer).name;
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
                    Label_SelectedPlayer.Text = GetGclient(CurrentPlayer).name;
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
                    Label_SelectedPlayer.Text = GetGclient(CurrentPlayer).name;
                }
            }
        }

        private void Btn_AddScore_Click(object sender, EventArgs e)
        {
            if (memory.IsProcessRunning(Program.GameName))
            {
                try
                {
                    GetGclient(CurrentPlayer).score += int.Parse(Text_Score.Text);
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
                    GetGclient(CurrentPlayer).score -= int.Parse(Text_Score.Text);
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
                    Int64 addr = memory.FindBinary("dxgi.dll", "55 57 41 56 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 60");

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
                        if (!GetGclient(CurrentPlayer).HasWeapon(Text_Weapon.Text))
                        {
                            GetGclient(CurrentPlayer).GiveWeapon(Text_Weapon.Text);
                            int weapSlot = GetGclient(CurrentPlayer).GetWeaponSlot(Text_Weapon.Text);
                            GetGclient(CurrentPlayer).ammoInClip[weapSlot] = 100000;
                            GetGclient(CurrentPlayer).ammoNotInClip[weapSlot] = 100000;

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
                    if (GetGclient(CurrentPlayer).HasWeapon(Text_Weapon.Text))
                    {
                        GetGclient(CurrentPlayer).TakeWeapon(Text_Weapon.Text);
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
                    GetGclient(CurrentPlayer).EnableInvulnerability();
                }
            }
        }

        private void Btn_GodmodeOFF_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    GetGclient(CurrentPlayer).DisableInvulnerability();
                }
            }
        }

        private void Btn_NoclipON_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    GetGclient(CurrentPlayer).EnableNoclip();
                }
            }
        }

        private void Btn_NoclipOFF_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    GetGclient(CurrentPlayer).DisableNoclip();
                }
            }
        }

        private void Btn_SmallCrosshairON_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    GetGclient(CurrentPlayer).SetSpreadOverride(0);
                }
            }
        }

        private void Btn_SmallCrosshairOFF_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    GetGclient(CurrentPlayer).ResetSpreadOverride();
                }
            }
        }

        private void Btn_RapidFireON_Click(object sender, EventArgs e)
        {
            brapid_fire[CurrentPlayer] = true;
        }

        private void Btn_RapidFireOFF_Click(object sender, EventArgs e)
        {
            brapid_fire[CurrentPlayer] = false;
        }

        private void Btn_UnlimitedAmmoON_Click(object sender, EventArgs e)
        {
            bunlimited_ammo[CurrentPlayer] = true;
        }

        private void Btn_UnlimitedAmmoOFF_Click(object sender, EventArgs e)
        {
            bunlimited_ammo[CurrentPlayer] = false;
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
                        GetGclient(CurrentPlayer).SetPerk(perknames[iPerk]);
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
                        GetGclient(CurrentPlayer).UnsetPerk(perknames[iPerk]);
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
                    GetGclient(CurrentPlayer).bThirdPerson = 1;
                }
            }
        }

        private void Btn_ThirdpersonOFF_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    GetGclient(CurrentPlayer).bThirdPerson = 0;
                }
            }
        }

        private void Btn_SavePosition_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    float[] vec = GetGclient(CurrentPlayer).origin;
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
                    GetGclient(CurrentPlayer).origin = vec;
                }
            }
        }

        private void Btn_DumpWeaponsForMap_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    string weapons = "";
                    for (int iWeap = 0; iWeap < bg_weaponDefs.Length; iWeap++)
                    {
                        weapons += $"{bg_weaponDefs[iWeap].szInternalName:X16}\n";
                    }
                    File.WriteAllText($"{Application.StartupPath}\\WeaponList.txt", weapons);
                    XtraMessageBox.Show($"Weapon Hashes dumped\n{Application.StartupPath}\\WeaponList.txt", "Weapons Dumped!");
                }
            }
        }

        private void Btn_DumpWeapons_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    gclient client = GetGclient(CurrentPlayer);

                    int weapIndex = (int)(client.currentWeapon & 0x3FF);
                    string currentWeapon = client.GetCurrentWeapon();

                    XtraMessageBox.Show($"Current Weapon: {currentWeapon:X16}", "Current Weapon");
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
