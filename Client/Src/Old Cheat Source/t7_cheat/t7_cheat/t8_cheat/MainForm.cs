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
using System.IO;
using DevExpress.XtraEditors;

namespace t8_cheat
{
    public partial class MainForm : XtraForm
    {
        private Memory memory = new Memory();
        private XAssetPool s_assetPools = null;
        public static WeaponDef bg_weaponDefs = null;
        private gclients[] g_clients = new gclients[4];
        private int SelectedPlayer = 0;
        public MainForm()
        {
            InitializeComponent();
            for (int iclient = 0;iclient < 4;iclient++)
            {
                g_clients[iclient] = new gclients(memory);
            }
            s_assetPools = new XAssetPool(memory);
            bg_weaponDefs = new WeaponDef(memory);
            new Task(new Action(ammo_thread)).Start();

        }

        gclients GetGClient(int clientNum)
        {
            g_clients[clientNum].initialize(clientNum);
            return g_clients[clientNum];
        }

        #region .. perk list ..
        public static Int64[] perknames = new Int64[] {     0x2DF64CB80991FFED,
                                                            0x08F0FD80C4F05A7A,
                                                            0x3DD40CDD95208D81,
                                                            0x4D243E134CCD2799,
                                                            0x4F61502D09299FF8,
                                                            0x4496B318AE6DE02C,
                                                            0x1D2A840A832FCC35,
                                                            0x4DB202C9135909C5,
                                                            0x55708FAB270CBDF1,
                                                            0x6B1FB0891CDEF14E,
                                                            0x2878E1FE5E5905FD,
                                                            0x2E809C12B1518F20,
                                                            0x14215F6359D1D996,
                                                            0x4B604942967B4454,
                                                            0x15E2B79A4781BBA6,
                                                            0x410E78A6016B77A6,
                                                            0x082D803AEEE9CAFB,
                                                            0x08F848566C99EE68,
                                                            0x3ACC18ECA92105F8,
                                                            0x12238C76A8027FED,
                                                            0x5E85A3BF4B529D08,
                                                            0x08D7A3AD21289785,
                                                            0x642983B360BC2E26,
                                                            0x5538EC25EB3D5810,
                                                            0x5163411D6D29E0A4,
                                                            0x65FA003530A80FCE,
                                                            0x39503D86B75EA8E8,
                                                            0x039FE88B0BC8A961,
                                                            0x640F35DC11FD63FF,
                                                            0x758926B842A1935D,
                                                            0x74954FA4BCF75B01,
                                                            0x58B5B4FEAC223D33,
                                                            0x7D321737E11F0259,
                                                            0x466D267255EFD956,
                                                            0x756CED3FE4BC8B08,
                                                            0x2518E706D82A5152,
                                                            0x05469CE15894186C,
                                                            0x3015DD885F613429,
                                                            0x14D17D597281F9A1,
                                                            0x26C1A269E73E85D8,
                                                            0x53275530CC552835,
                                                            0x1B1E3985DED29427,
                                                            0x0531B9692E0BD06F,
                                                            0x25BBFA65D8A1795D,
                                                            0x175888A682967E25,
                                                            0x5C6DDDA6A9A80574,
                                                            0x4A696CC60A287AC0,
                                                            0x67B6BD46D61789C6,
                                                            0x33547B925AA16A42,
                                                            0x334E57071A761866,
                                                            0x6D26AB564DE898C1,
                                                            0x526A95ABAE2DF17C,
                                                            0x405B9BFE2815ACFB,
                                                            0x0447E63437D3FBF7,
                                                            0x76D3B15629DBAF20,
                                                            0x0F3E6AB220AB2E9A,
                                                            0x59F7764FBF2A4BED,
                                                            0x1472D269339F49A2,
                                                            0x0F7503C6D57F1549,
                                                            0x3ABBCC4BD4544F70,
                                                            0x19FED41143EDFA28,
                                                            0x7DD2B4A22E1C8D5A,
                                                            0x4EF6AE6C8304444A,
                                                            0x72062BF89AB6D89C,
                                                            0x642C47473C75FE73,
                                                            0x6D5A653FC4A4CE2B,
                                                            0x05A44A9C2F315C4C,
                                                            0x4178C5CB867EB187,
                                                            0x5DF7A632E2FE76AA,
                                                            0x36D2D747B066FDF8,
                                                            0x332FC523C4D71D6C,
                                                            0x77FFCD7C476BED9B,
                                                            0x12AA651882801A93,
                                                            0x11BA78D175E4720A,
                                                            0x4A2629A79E9730FA,
                                                            0x6FEB2B83ED5CDF88,
                                                            0x5125802D314FC0DB,
                                                            0x0CD430AA623CEE9B,
                                                            0x348E0C6F4F1B29AF,
                                                            0x3A569A25047F2B0E,
                                                            0x24612907323DF08E,
                                                            0x33D0C74F0101B5BF,
                                                            0x28FC3B35F747E2D7,
                                                            0x1635C9573BA4FA60,
                                                            0x2ECBF79FF01F5A20,
                                                            0x636C467D06804130,
                                                            0x3DAF3C37DB7F5C49,
                                                            0x410C46B5FF702C96,
                                                            0x4A86520DEBBD6FBA,
                                                            0x485819932A6D9597,
                                                            0x4F79622F34730BA6,
                                                            0x252FB195D11B95A3,
                                                            0x171FB0B6B2454A45,
                                                            0x08FA8974D0C8C2D4,
                                                            0x6BA53ABE46409FB8,
                                                            0x3E2E0C06FCBF9B30,
                                                            0x3B96130ECD64FA0C,
                                                            0x43F323255A0F43BF,
                                                            0x44F0A6116AB4D51E,
                                                            0x25CF232C312C6F4B,
                                                            0x7924D72227E33A59,
                                                            0x2CB93D425498EA7F,
                                                            0x147EFF6D26DDD670,
                                                            0x73A58AE52A6859AB,
                                                            0x0C8D6AF1F3D58843,
                                                            0x3A9C7CEB0A89F9EC,
                                                            0x47F1A3BB8647D2E7,
                                                            0x0C1C18495F81BD91,
                                                            0x6C37C7AD7A9E4ACC,
                                                            0x3C3F73D1554BBA50,
                                                            0x27D77C4D89AF25D9,
                                                            0x59AB4A474C550CE8,
                                                            0x4366F3EE0DA82A33,
                                                            0x463B12460F4EDA0D,
                                                            0x314C31D48DAFA443,
                                                            0x7EF536F9AE25F7CE,
                                                            0x3C1230FD0A6E4465,
                                                            0x28E793E610D18F61,
                                                            0x1A1660312A4DB585,
                                                            0x49E0CF537BBBE8E5,
                                                            0x474A3F888C278B74,
                                                            0x1B4E14022CC30D96,
                                                            0x4AEC28C0180F6F80,
                                                            0x3E3005511CC2A5D7,
                                                            0x3ECE1AEE9A7BEDCD,
                                                            0x407F121F24685B57,
                                                            0x4DF21972DD2A3A87,
                                                            0x13D4A1427EDA9882,
                                                            0x37AA3A5919757781,
                                                            0x5E2BD0CCEF40C27A,
                                                            0x3BBDE08AB2910872,
                                                            0x0A6796D796A8D2B9,
                                                            0x14E2498426FADEA3,
                                                            0x3E2F955CD85B5532,
                                                            0x5266C7139F9ACF56,
                                                            0x300C4E868F92134B,
                                                            0x3EAC84D6FE51944B,
                                                            0x301AAA36FAE44A5C,
                                                            0x5F92F41496100E22,
                                                            0x34C7D1E8A059F87E,
                                                            0x5706909BC1DB0F85,
                                                            0x5B141F82A55645A9,
                                                            0x57F8288B71496FE9,
                                                            0x66E6FBE3CC2AFF65,
                                                            0x6CA140703A87CD09,
                                                            0x0CD6E82BD4CE7ABF,
                                                            0x6AFC24062D2515A2,
                                                            0x48255A3B086A9BEA,
                                                            0x4262DC5DC4ACB784,
                                                            0x6C2D5BB068417557,
                                                            0x593AC4823FE89272,
                                                            0x559086CCB08F32BA,
                                                            0x650A97787905913F,
                                                            0x4EF368F54CD86AB7,
                                                            0x2EA01DCD66EB31BC,
                                                            0x4D0137790124D3F5,
                                                            0x153B9C0C558DB313,
                                                            0x48D89FDA4346187F,
                                                            0x5321E235B64BB84D,
                                                            0x3A09B1D7EAA88087,
                                                            0x6DA63D760C1788E2,
                                                            0x7036CE8DAFBF1F81,
                                                            0x53010725C65A98A5,
                                                            0x130074EC6DE7A431,
                                                            0x1BC7D0EA42D1D0A8,
                                                            0x36B9957A693185EA,
                                                            0x4C14ED37C4038671,
                                                            0x4519DC1D3AC79139,
                                                            0x4723E346254CB334,
                                                            0x377149A415143F1B,
                                                            0x1B2D5C9444AC98F2 };
        #endregion

        void ammo_thread()
        {
            while (true)
            {
                if (memory.IsSafe())
                {
                    if (memory.IsProcessRunning(Program.GameName))
                    {
                        gclients client = GetGClient(0);
                        Int64 weapHash = client.GetCurrentWeapon();
                        int weapSlot = client.GetWeaponSlot(weapHash);

                        if (client.bRapidFire)
                        {
                            client.weapTime = 0;
                        }

                        if (client.bUnlimitedAmmo)
                        {
                            client.ammoInClip[weapSlot] = bg_weaponDefs[(int)(client.currentWeapon & 0x3FF)].defs.iClipSize;
                        }
                    }
                }
                Thread.Sleep(40);
            }
        }

        private void Btn_DumpWeapons_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    gclients client = GetGClient(0);
                    client.initialize(0);
                    int weapIndex = (int)(client.currentWeapon & 0x3FF);
                    Int64 currentWeapon = client.GetCurrentWeapon();
                    int weapSlot = client.GetWeaponSlot(currentWeapon);

                    XtraMessageBox.Show($"Current Weapon: {currentWeapon:X16}", "Current Weapon");
                }
            }
        }

        private void Btn_Give_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    Int64 weapHash = Int64.Parse(Text_Weapon.Text, System.Globalization.NumberStyles.HexNumber);
                    gclients client = GetGClient(SelectedPlayer);
                    try
                    {
                        int weapIndex = bg_weaponDefs.GetIndexForHash(weapHash);
                        if (weapIndex != -1)
                        {
                            if (!client.HasWeapon(weapHash))
                            {
                                client.GiveWeapon(weapHash);
                                int weapSlot = client.GetWeaponSlot(weapHash);
                                client.ammoNotInClip[weapSlot].ammo = 100000;

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
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message} - {ex.HResult:X}");
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
                    Int64 weapHash = Int64.Parse(Text_Weapon.Text, System.Globalization.NumberStyles.HexNumber);
                    gclients client = GetGClient(SelectedPlayer);
                    try
                    {
                        if (client.HasWeapon(weapHash))
                        {
                            client.TakeWeapon(weapHash);
                        }
                        else
                        {
                            XtraMessageBox.Show("You do not have that weapon therefore you cannot take it away!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message} - {ex.HResult:X}");
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
                    gclients client = GetGClient(SelectedPlayer);
                    client.EnableInvulnerability();
                }
            }
        }

        private void Btn_GodmodeOFF_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    gclients client = GetGClient(SelectedPlayer);
                    client.DisableInvulnerability();
                }
            }
        }

        private void Btn_NoclipON_Click(object sender, EventArgs e)
        {

        }

        private void Btn_NoclipOFF_Click(object sender, EventArgs e)
        {

        }

        private void Btn_RapidFireON_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    g_clients[SelectedPlayer].bRapidFire = true;
                }
            }
        }

        private void Btn_RapidFireOFF_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    g_clients[SelectedPlayer].bRapidFire = false;
                }
            }
        }

        private void Btn_UnlimitedAmmoON_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    g_clients[SelectedPlayer].bUnlimitedAmmo = true;
                }
            }
        }

        private void Btn_UnlimitedAmmoOFF_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    g_clients[SelectedPlayer].bUnlimitedAmmo = true;
                }
            }
        }

        private void Btn_RemoveScore_Click(object sender, EventArgs e)
        {
            if (memory.IsProcessRunning(Program.GameName))
            {
                try
                {
                    gclients client = GetGClient(SelectedPlayer);
                    client.score -= int.Parse(Text_Score.Text);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }

        private void Btn_AddScore_Click(object sender, EventArgs e)
        {
            if (memory.IsProcessRunning(Program.GameName))
            {
                try
                {
                    gclients client = GetGClient(SelectedPlayer);
                    client.score += int.Parse(Text_Score.Text);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
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
                    gclients client = GetGClient(SelectedPlayer);
                    for (int iPerk = 0; iPerk < perknames.Length; iPerk++)
                    {
                        if (!client.HasPerk(perknames[iPerk]))
                        {
                            client.SetPerk(perknames[iPerk]);
                        }
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
                    gclients client = GetGClient(SelectedPlayer);
                    for (int iPerk = 0; iPerk < perknames.Length; iPerk++)
                    {
                        if (client.HasPerk(perknames[iPerk]))
                        {
                            client.UnsetPerk(perknames[iPerk]);
                        }
                    }
                    client.SetPerk(0x7924D72227E33A59); //unknown
                    client.SetPerk(0x49E0CF537BBBE8E5); //sliding
                    client.SetPerk(0x474A3F888C278B74); //sprinting
                }
            }
        }

        private void Btn_ThirdpersonON_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    gclients client = GetGClient(SelectedPlayer);
                    client.bThirdPerson = 1;
                }
            }
        }

        private void Btn_ThirdpersonOFF_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    gclients client = GetGClient(SelectedPlayer);
                    client.bThirdPerson = 0;
                }
            }
        }

        private void Btn_SavePosition_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    gclients client = GetGClient(SelectedPlayer);
                    client.SaveLocation._x = client.origin.x;
                    client.SaveLocation._y = client.origin.y;
                    client.SaveLocation._z = client.origin.z;
                }
            }
        }

        private void Btn_LoadPosition_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    gclients client = GetGClient(SelectedPlayer);
                    float[] pos = new float[3] { client.SaveLocation._x, client.SaveLocation._y, client.SaveLocation._z };
                    client._origin = pos;
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
                        weapons += $"{bg_weaponDefs[iWeap].szInternalName:X16} - {bg_weaponDefs[iWeap].defs.iClipSize}\n";
                    }
                    File.WriteAllText($"{Application.StartupPath}\\WeaponList.txt", weapons);

                    XtraMessageBox.Show($"Weapon Hashes dumped\n{Application.StartupPath}\\WeaponList.txt", "Weapons Dumped!");
                }
            }
        }

        private void Btn_SelectPlayer1_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    SelectedPlayer = 0;
                    gclients client = GetGClient(SelectedPlayer);
                    Label_SelectedPlayer.Text = client.name;
                }
            }
        }

        private void Btn_SelectPlayer2_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    SelectedPlayer = 1;
                    gclients client = GetGClient(SelectedPlayer);
                    Label_SelectedPlayer.Text = client.name;
                }
            }
        }

        private void Btn_SelectPlayer3_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    SelectedPlayer = 2;
                    gclients client = GetGClient(SelectedPlayer);
                    Label_SelectedPlayer.Text = client.name;
                }
            }
        }

        private void Btn_SelectPlayer4_Click(object sender, EventArgs e)
        {
            if (memory.IsSafe())
            {
                if (memory.IsProcessRunning(Program.GameName))
                {
                    SelectedPlayer = 3;
                    gclients client = GetGClient(SelectedPlayer);
                    Label_SelectedPlayer.Text = client.name;
                }
            }
        }

        private void Page_ClientList_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
