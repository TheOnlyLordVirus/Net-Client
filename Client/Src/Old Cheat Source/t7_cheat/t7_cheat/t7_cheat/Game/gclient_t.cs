using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t7_cheat
{
    
    class AmmoPool
    {
        public Int64 ammoBase;
        private Memory memory = null;
        public AmmoPool(Memory mem, Int64 ammoStart)
        {
            ammoBase = ammoStart;
            memory = mem;
        }
        public int this[int index]
        {
            get { return memory.ReadInt(ammoBase + (4 * index)); }
            set { memory.WriteInt(ammoBase + (4 * index), value); }
        }
    }

    class Weapons
    {
        public Int64 weaponBase;
        private Memory memory = null;
        public Weapons(Memory mem, Int64 weapStart)
        {
            weaponBase = weapStart;
            memory = mem;
        }
        public Int64 this[int index]
        {
            get { return memory.ReadPointer(weaponBase + (0x30 * index));  }
            set { memory.WritePointer(weaponBase + (0x30 * index), value); }
        }
    }
    class gclient
    {
        private Memory memory = null;
        public gclient(Memory mem)
        {
            memory = mem;
            //Console.WriteLine($"{memory:X} {mem:X}");
        }

        public Int64 clientBase;
        private int clientNum;

        public enum Addresses
        {
            gclient_a = 0xA54BDE0,


            m_clientNum = 0x00,
            m_commandTime = 0x04,
            m_pmtype = 0x08,
            m_bobCycle = 0xC,
            m_pmflags = 0x10,
            m_weapFlags = 0x18,
            m_otherFlags = 0x20,
            m_pmtime = 0x24,

            m_origin = 0x30,
            m_velocity = 0x3C,

            m_weapTime = 0x54,
            m_thirdperson = 0xBC,
            m_body = 0x2A8,

            m_weapon = 0x2C0,

            m_viewangles = 0x318,

            m_spreadOverride = 0x30C,
            m_spreadOverrideState = 0x310,

            m_weapons = 0x378,
            m_ammoNotInClip = 0x648,
            m_ammoInClip = 0x684,

            m_playerEnergyRatio = 0x884,
            m_playerEnergyTime = 0x888,
            m_aimSpreadScale = 0xB78,

            m_perks = 0xAF4,
            m_perks1 = 0x16C8C,

            m_name = 0x00016BB8,

            m_Score = 0x16E84,

            m_Kills1 = 0x16E88,
            m_Kills2 = 0x16E98,
            m_Kills3 = 0x16F40,

            m_Headshots1 = 0x00016EA4,
            m_Headshots2 = 0x00016EC0,
            m_Headshots3 = 0x00016F90,

            m_moveSpeedScale = 0x00016BEC,
            m_buttonBits = 0x00016B14,

            m_noclipState = 0x00016FE4,
        }

        public gclient this[int index]
        {
            get
            {
                clientNum = index;
                Int64 baseAddr = memory.CurrentProcess.MainModule.BaseAddress.ToInt64();
                clientBase = (memory.ReadPointer(baseAddr + (Int64)Addresses.gclient_a) + (index * 0x171F0));
                //Console.WriteLine($"Accessing gclient with index {index} {clientBase:X} - {baseAddr:X}");

                return this;
            }
        }
        public gclient GetClient(int index)
        {
            Int64 baseAddr = memory.CurrentProcess.MainModule.BaseAddress.ToInt64();
            clientBase = (memory.ReadPointer(baseAddr + (Int64)Addresses.gclient_a) + (index * 0x171F0));
            return this;
        }

        public int commandTime
        {
            get { return memory.ReadInt(clientBase + (int)Addresses.m_commandTime); }
        }
        public int pm_type
        {
            get { return memory.ReadInt(clientBase + (int)Addresses.m_pmtype); }
        }
        public int bobCycle
        {
            get { return memory.ReadInt(clientBase + (int)Addresses.m_bobCycle); }
        }
        public Int64 pm_flags
        {
            get { return memory.ReadPointer(clientBase + (int)Addresses.m_pmflags); }
        }
        public Int64 weapFlags
        {
            get { return memory.ReadPointer(clientBase + (int)Addresses.m_weapFlags); }
        }
        public uint otherFlags
        {
            get { return memory.ReadUInt(clientBase + (int)Addresses.m_otherFlags); }
            set { memory.WriteUInt(clientBase + (int)Addresses.m_otherFlags, value); }
        }
        public int pm_time
        {
            get { return memory.ReadInt(clientBase + (int)Addresses.m_pmtime); }
        }
        public int spreadOverride
        {
            get { return memory.ReadInt(clientBase + (int)Addresses.m_spreadOverride); }
            set { memory.WriteInt(clientBase + (int)Addresses.m_spreadOverride, value); }
        }
        public int spreadOverrideState
        {
            get { return memory.ReadInt(clientBase + (int)Addresses.m_spreadOverrideState); }
            set { memory.WriteInt(clientBase + (int)Addresses.m_spreadOverrideState, value); }
        }
        public Weapons weapons
        {
            get
            {
                Weapons weap = new Weapons(memory, clientBase + (int)Addresses.m_weapons);
                return weap;
            }
        }
        public AmmoPool ammoNotInClip
        {
            get
            {
                AmmoPool weap = new AmmoPool(memory, clientBase + (int)Addresses.m_ammoNotInClip);
                return weap;
            }
        }
        public AmmoPool ammoInClip
        {
            get
            {
                AmmoPool weap = new AmmoPool(memory, clientBase + (int)Addresses.m_ammoInClip);
                return weap;
            }
        }
        public float playerEnergyRatio
        {
            get { return memory.ReadFloat(clientBase + (int)Addresses.m_playerEnergyRatio); }
            set { memory.WriteFloat(clientBase + (int)Addresses.m_playerEnergyRatio, value); }
        }
        public int playerEnergyTime
        {
            get { return memory.ReadInt(clientBase + (int)Addresses.m_playerEnergyTime); }
            set { memory.WriteFloat(clientBase + (int)Addresses.m_playerEnergyTime, value); }
        }
        public float aimSpreadScale
        {
            get { return memory.ReadFloat(clientBase + (int)Addresses.m_aimSpreadScale); }
            set { memory.WriteFloat(clientBase + (int)Addresses.m_aimSpreadScale, value); }
        }
        public uint perks1
        {
            get { return memory.ReadUInt(clientBase + (int)Addresses.m_perks + 0); }
            set
            {
                memory.WriteUInt(clientBase + (int)Addresses.m_perks + 0, value);
                memory.WriteUInt(clientBase + (int)Addresses.m_perks1 + 0, value);
            }
        }
        public uint perks2
        {
            get { return memory.ReadUInt(clientBase + (int)Addresses.m_perks + 4); }
            set {
                memory.WriteUInt(clientBase + (int)Addresses.m_perks + 4, value);
                memory.WriteUInt(clientBase + (int)Addresses.m_perks1 + 4, value);
            }
        }
        public uint perks3
        {
            get { return memory.ReadUInt(clientBase + (int)Addresses.m_perks + 8); }
            set {
                memory.WriteUInt(clientBase + (int)Addresses.m_perks + 8, value);
                memory.WriteUInt(clientBase + (int)Addresses.m_perks1 + 8, value);
            }
        }
        public uint perks4
        {
            get { return memory.ReadUInt(clientBase + (int)Addresses.m_perks + 12); }
            set {
                memory.WriteUInt(clientBase + (int)Addresses.m_perks + 12, value);
                memory.WriteUInt(clientBase + (int)Addresses.m_perks1 + 12, value);
            }
        }
        public int score
        {
            get { return memory.ReadInt(clientBase + (int)Addresses.m_Score); }
            set { memory.WriteInt(clientBase + (int)Addresses.m_Score, value); }
        }
        public int kills
        {
            get { return memory.ReadInt(clientBase + (int)Addresses.m_Kills1); }
            set
            {
                memory.WriteInt(clientBase + (int)Addresses.m_Kills1, value);
                memory.WriteInt(clientBase + (int)Addresses.m_Kills2, value);
                memory.WriteInt(clientBase + (int)Addresses.m_Kills3, value);
            }
        }
        public int headshots
        {
            get { return memory.ReadInt(clientBase + (int)Addresses.m_Headshots1); }
            set
            {
                memory.WriteInt(clientBase + (int)Addresses.m_Headshots1, value);
                memory.WriteInt(clientBase + (int)Addresses.m_Headshots2, value);
                memory.WriteInt(clientBase + (int)Addresses.m_Headshots3, value);
            }
        }
        public float moveSpeedScale
        {
            get { return memory.ReadFloat(clientBase + (int)Addresses.m_moveSpeedScale); }
            set { memory.WriteFloat(clientBase + (int)Addresses.m_moveSpeedScale, value); }
        }
        public uint[] button_bits
        {
            get
            {
                uint[] ret = new uint[3];
                ret[0] = memory.ReadUInt(clientBase + (int)Addresses.m_buttonBits + 0);
                ret[1] = memory.ReadUInt(clientBase + (int)Addresses.m_buttonBits + 4);
                ret[2] = memory.ReadUInt(clientBase + (int)Addresses.m_buttonBits + 8);
                return ret;
            }
        }
        public float[] origin
        {
            get
            {
                float[] vec = new float[3] { memory.ReadFloat(clientBase + (int)Addresses.m_origin + 0), memory.ReadFloat(clientBase + (int)Addresses.m_origin + 4), memory.ReadFloat(clientBase + (int)Addresses.m_origin + 8) };
                return vec;
            }
            set
            {
                memory.WriteFloat(clientBase + (int)Addresses.m_origin + 0, value[0]);
                memory.WriteFloat(clientBase + (int)Addresses.m_origin + 4, value[1]);
                memory.WriteFloat(clientBase + (int)Addresses.m_origin + 8, value[2]);
            }
        }
        public float[] velocity
        {
            get
            {
                float[] vec = new float[3] { memory.ReadFloat(clientBase + (int)Addresses.m_velocity + 0), memory.ReadFloat(clientBase + (int)Addresses.m_velocity + 4), memory.ReadFloat(clientBase + (int)Addresses.m_velocity + 8) };
                return vec;
            }
            set
            {
                memory.WriteFloat(clientBase + (int)Addresses.m_velocity + 0, value[0]);
                memory.WriteFloat(clientBase + (int)Addresses.m_velocity + 4, value[1]);
                memory.WriteFloat(clientBase + (int)Addresses.m_velocity + 8, value[2]);
            }
        }
        public float[] viewangles
        {
            get
            {
                float[] vec = new float[3] { memory.ReadFloat(clientBase + (int)Addresses.m_viewangles + 0), memory.ReadFloat(clientBase + (int)Addresses.m_viewangles + 4), memory.ReadFloat(clientBase + (int)Addresses.m_viewangles + 8) };
                return vec;
            }
            set
            {
                memory.WriteFloat(clientBase + (int)Addresses.m_viewangles + 0, value[0]);
                memory.WriteFloat(clientBase + (int)Addresses.m_viewangles + 4, value[1]);
                memory.WriteFloat(clientBase + (int)Addresses.m_viewangles + 8, value[2]);
            }
        }
        public int weapTime
        {
            get { return memory.ReadInt(clientBase + (int)Addresses.m_weapTime); }
            set { memory.WriteInt(clientBase + (int)Addresses.m_weapTime, value); }
        }
        public Int64 currentWeapon
        {
            get { return memory.ReadPointer(clientBase + (int)Addresses.m_weapon); }
            set { memory.WritePointer(clientBase + (int)Addresses.m_weapon, value); }
        }
        public string name
        {
            get
            {
                string pName = "Not Connected";
                string str = memory.ReadString(clientBase + (int)Addresses.m_name);
                if (!str.Equals(""))
                {
                    pName = str;
                }
                return pName;
            }
        }
        public uint nflags
        {
            get
            {
                return memory.ReadUInt(clientBase + (int)Addresses.m_noclipState);
            }
            set
            {
                memory.WriteUInt(clientBase + (int)Addresses.m_noclipState, value);
            }
        }
        public byte bThirdPerson
        {
            get { return memory.ReadByte(clientBase + (int)Addresses.m_thirdperson); }
            set { memory.WriteByte(clientBase + (int)Addresses.m_thirdperson, value); }
        }

        private int BG_GetPerkIndexForName(string perkName)
        {
            for (int iPerk = 0;iPerk < MainForm.perknames.Length;iPerk++)
            {
                if (perkName == MainForm.perknames[iPerk])
                {
                    return iPerk;
                }
            }
            return -1;
        }
        public int GetWeaponSlot(string weapName)
        {
            int weapIndex = MainForm.bg_weaponDefs.GetIndexForName(weapName);
            if (weapIndex != -1)
            {
                for (int i = 0; i < 15; i++)
                {
                    if (this.weapons[i] == weapIndex)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        public void GiveWeapon(string weapName)
        {
            int weapIndex = MainForm.bg_weaponDefs.GetIndexForName(weapName);
            if (weapIndex != -1)
            {
                for (int i = 0; i < 15; i++)
                {
                    if (this.weapons[i] != weapIndex)
                    {
                        if (this.weapons[i] == 0)
                        {
                            Console.WriteLine($"{clientBase:X} was given weapon {weapName}");
                            this.weapons[i] = weapIndex;
                            break;
                        }
                    }
                }
            }
        }
        public void MaxAmmo()
        {
            for (int i = 0; i < 15; i++)
            {
                if (this.weapons[i] != 0)
                {
                    this.ammoInClip[i] = MainForm.bg_weaponDefs[(int)(this.weapons[i] & 0x1FF)].iClipSize;
                }
            }
        }
        public void TakeWeapon(string weapName)
        {
            int weapIndex = MainForm.bg_weaponDefs.GetIndexForName(weapName);
            int weapSlot = this.GetWeaponSlot(weapName);
            if (weapIndex != -1 && weapSlot != -1)
            {
                this.weapons[weapSlot] = 0;
                this.ammoInClip[weapSlot] = 0;
                this.ammoNotInClip[weapSlot] = 0;
            }
        }
        public bool HasWeapon(string weapName)
        {
            int weapIndex = MainForm.bg_weaponDefs.GetIndexForName(weapName);
            if (weapIndex != -1)
            {
                for (int i = 0; i < 15; i++)
                {
                    if (this.weapons[i] == weapIndex)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void SetPerk(string perkName)
        {
            int perkIndex = BG_GetPerkIndexForName(perkName);
            int perkSlot = (perkIndex >> 5);
            uint perkValue = (uint)(1 << (perkIndex & 0x1F));

            Console.WriteLine($"Client {clientBase:X} setting perk \"{perkName}\" on slot {perkSlot} with value {perkValue}");

            switch (perkSlot)
            {
                case 0: this.perks1 |= perkValue; break;
                case 1: this.perks2 |= perkValue; break;
                case 2: this.perks3 |= perkValue; break;
                case 3: this.perks4 |= perkValue; break;
            }
        }
        public void UnsetPerk(string perkName)
        {
            int perkIndex = BG_GetPerkIndexForName(perkName);
            int perkSlot = (perkIndex >> 5);
            uint perkValue = ~(uint)(1 << (perkIndex & 0x1F));

            Console.WriteLine($"Client {clientBase:X} setting perk \"{perkName}\" on slot {perkSlot} with value {perkValue}");

            switch (perkSlot)
            {
                case 0: this.perks1 &= perkValue; break;
                case 1: this.perks2 &= perkValue; break;
                case 2: this.perks3 &= perkValue; break;
                case 3: this.perks4 &= perkValue; break;
            }
        }
        public void EnableInvulnerability()
        {
            this.otherFlags |= 1;
        }
        public void DisableInvulnerability()
        {
            this.otherFlags &= 0xFFFFFFFE;
        }
        public void SetSpreadOverride(int spread)
        {
            this.spreadOverride = spread;
            this.spreadOverrideState = 2;
        }
        public void ResetSpreadOverride()
        {
            this.spreadOverrideState = 1;
            this.aimSpreadScale = 255.0f;
        }
        public void EnableNoclip()
        {
            this.nflags |= 1;
        }
        public void DisableNoclip()
        {
            this.nflags &= 0xFFFFFFFE;
        }
        public string GetCurrentWeapon()
        {
            return MainForm.bg_weaponDefs[(int)(this.currentWeapon & 0x1FF)].szInternalName;
        }
    }
}
