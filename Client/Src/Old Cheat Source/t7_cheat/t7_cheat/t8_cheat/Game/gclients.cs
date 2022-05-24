using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t8_cheat
{
    class Vector
    {
        public Int64 vecBase;
        private Memory memory = null;
        public Vector()
        {

        }
        public Vector(Memory mem)
        {
            memory = mem;
        }

        public float _x;
        public float _y;
        public float _z;

        public float x
        {
            get { return memory.ReadFloat(vecBase + 0); }
            set { Console.WriteLine($"{vecBase:X}"); memory.WriteFloat(vecBase + 0, value); }
        }
        public float y
        {
            get { return memory.ReadFloat(vecBase + 4); }
            set { memory.WriteFloat(vecBase + 4, value); }
        }
        public float z
        {
            get { return memory.ReadFloat(vecBase + 8); }
            set { memory.WriteFloat(vecBase + 8, value); }
        }

    }

    class AmmoPool
    {
        public Int64 ammoBase;
        private Memory memory = null;
        public AmmoPool(Memory mem)
        {
            memory = mem;
        }
        public int this[int index]
        {
            get { return memory.ReadInt(ammoBase + (4 * index)); }
            set { memory.WriteInt(ammoBase + (4 * index), value); }
        }
    }

    class StockAmmoPool
    {
        public Int64 ammoBase;
        private Memory memory = null;
        private int selected;
        public StockAmmoPool(Memory mem)
        {
            memory = mem;
        }
        public StockAmmoPool this[int index]
        {
            get { selected = index; return this; }
            //set { memory.WriteInt(ammoBase + (8 * index), value); }
        }

        public int ammoType
        {
            get
            {
                return memory.ReadInt(ammoBase + (selected * 8) + 0);
            }
            set
            {
                memory.WriteInt(ammoBase + (selected * 8) + 0, value);
            }
        }

        public int ammo
        {
            get
            {
                return memory.ReadInt(ammoBase + (selected * 8) + 4);
            }
            set
            {
                memory.WriteInt(ammoBase + (selected * 8) + 4, value);
            }
        }
    }

    class Weapons
    {
        public Int64 weaponBase;
        private Memory memory = null;
        public Weapons(Memory mem)
        {
            memory = mem;
        }
        public Int64 this[int index]
        {
            get { return memory.ReadPointer(weaponBase + (0x38 * index)); }
            set { memory.WritePointer(weaponBase + (0x38 * index), value); }
        }
    }
    class gclients
    {
        public int clientNum;
        private AmmoPool m_ammoInClip = null;
        private StockAmmoPool m_ammoNotInClip = null;
        private Weapons weaps = null;
        private Memory memory = null;
        public Vector m_origin = null;
        public Vector m_velocity = null;
        private Int64 clientBase = 0;
        public gclients(Memory mem)
        {
            memory = mem;
            m_ammoInClip = new AmmoPool(memory);
            m_ammoNotInClip = new StockAmmoPool(memory);
            weaps = new Weapons(memory);
            m_origin = new Vector(memory);
            m_velocity = new Vector(memory);
        }


        public bool bUnlimitedAmmo;
        public bool bRapidFire;
        public Vector SaveLocation = new Vector();

        public void initialize(int clientNum)
        {
            Int64 baseAddr = memory.CurrentProcess.MainModule.BaseAddress.ToInt64();
            clientBase = (memory.ReadPointer(baseAddr + (Int64)Addresses.gclient_a) + (clientNum * 0x4B50));
        }

        public enum Addresses
        {
            gclient_a = 0x9353A00,

            m_otherFlags = 0xE40,

            m_origin = 0xDD0,
            m_velocity = 0xDDC,

            m_weapTime = 0xE48,
            m_thirdperson = 0x153C,

            m_weapon = 0x20,

            m_viewangles = 0xDF4,


            m_weapons = 0x60,
            m_ammoNotInClip = 0x12AC,
            m_ammoInClip = 0x1324,

            m_perks = 0x1024,
            m_perks1 = 0x461C,

            m_name = 0x43AA,

            m_Score = 0x446C,

            m_moveSpeedScale = 0x43DC,
            m_buttonBits = 0x42EC,

        }
        #region .. functions ..
        public void GiveWeapon(Int64 weapName)
        {
            int weapIndex = MainForm.bg_weaponDefs.GetIndexForHash(weapName);
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
                            this.ammoNotInClip[i].ammoType = (int)MainForm.bg_weaponDefs[weapIndex].defs.weapAmmoType;
                            break;
                        }
                    }
                }
            }
        }

        public void TakeWeapon(Int64 weapName)
        {
            int weapIndex = MainForm.bg_weaponDefs.GetIndexForHash(weapName);
            int weapSlot = this.GetWeaponSlot(weapName);
            if (weapIndex != -1 && weapSlot != -1)
            {
                this.weapons[weapSlot] = 0;
                this.ammoInClip[weapSlot] = 0;
                this.ammoNotInClip[weapSlot].ammo = 0;
                this.ammoNotInClip[weapSlot].ammoType = 0;
            }
        }

        public bool HasWeapon(Int64 weapName)
        {
            int weapIndex = MainForm.bg_weaponDefs.GetIndexForHash(weapName);
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

        public int GetWeaponSlot(Int64 weapName)
        {
            int weapIndex = MainForm.bg_weaponDefs.GetIndexForHash(weapName);
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

        public Int64 GetCurrentWeapon()
        {
            return MainForm.bg_weaponDefs[(int)(this.currentWeapon & 0x3FF)].szInternalName;
        }
        public void EnableInvulnerability()
        {
            this.otherFlags |= 1;
        }
        public void DisableInvulnerability()
        {
            this.otherFlags &= 0xFFFFFFFE;
        }

        private int BG_GetPerkIndexForName(Int64 perkName)
        {
            for (int iPerk = 0; iPerk < MainForm.perknames.Length; iPerk++)
            {
                if (perkName == MainForm.perknames[iPerk])
                {
                    return iPerk;
                }
            }
            return -1;
        }

        public void SetPerk(Int64 perkName)
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
        public void UnsetPerk(Int64 perkName)
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

        public bool HasPerk(Int64 perkName)
        {
            int perkIndex = BG_GetPerkIndexForName(perkName);
            int perkSlot = (perkIndex >> 5);
            uint perkValue = (uint)(1 << (perkIndex & 0x1F));


            switch (perkSlot)
            {
                case 0: if ((this.perks1 & perkValue) != 0) { return true; } break;
                case 1: if ((this.perks2 & perkValue) != 0) { return true; } break;
                case 2: if ((this.perks3 & perkValue) != 0) { return true; } break;
                case 3: if ((this.perks4 & perkValue) != 0) { return true; } break;
            }
            return false;
        }

        #endregion

        #region .. properties ..
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
            set
            {
                memory.WriteUInt(clientBase + (int)Addresses.m_perks + 4, value);
                memory.WriteUInt(clientBase + (int)Addresses.m_perks1 + 4, value);
            }
        }
        public uint perks3
        {
            get { return memory.ReadUInt(clientBase + (int)Addresses.m_perks + 8); }
            set
            {
                memory.WriteUInt(clientBase + (int)Addresses.m_perks + 8, value);
                memory.WriteUInt(clientBase + (int)Addresses.m_perks1 + 8, value);
            }
        }
        public uint perks4
        {
            get { return memory.ReadUInt(clientBase + (int)Addresses.m_perks + 12); }
            set
            {
                memory.WriteUInt(clientBase + (int)Addresses.m_perks + 12, value);
                memory.WriteUInt(clientBase + (int)Addresses.m_perks1 + 12, value);
            }
        }

        public string name
        {
            get
            {
                string pName = memory.ReadString(clientBase + (int)Addresses.m_name);
                if (pName != "")
                {
                    return pName;
                }
                return "Not Connected";
            }
        }

        public int score
        {
            get
            {
                return memory.ReadInt(clientBase + (int)Addresses.m_Score);
            }
            set
            {
                memory.WriteInt(clientBase + (int)Addresses.m_Score, value);
            }
        }

        public int weapTime
        {
            get
            {
                return memory.ReadInt(clientBase + (int)Addresses.m_weapTime);
            }
            set
            {
                memory.WriteInt(clientBase + (int)Addresses.m_weapTime, value);
            }
        }

        public uint otherFlags
        {
            get { return memory.ReadUInt(clientBase + (int)Addresses.m_otherFlags); }
            set { memory.WriteUInt(clientBase + (int)Addresses.m_otherFlags, value); }
        }
        public byte bThirdPerson
        {
            get { return memory.ReadByte(clientBase + (int)Addresses.m_thirdperson); }
            set { memory.WriteByte(clientBase + (int)Addresses.m_thirdperson, value); }
        }

        public Int64 currentWeapon
        {
            get
            {
                return memory.ReadPointer(clientBase + (int)Addresses.m_weapon);
            }
        }

        public Weapons weapons
        {
            get
            {
                weaps.weaponBase = (clientBase + (int)Addresses.m_weapons);
                return weaps;
            }
        }

        public AmmoPool ammoInClip
        {
            get
            {
                m_ammoInClip.ammoBase = (clientBase + (int)Addresses.m_ammoInClip);
                return m_ammoInClip;
            }
        }
        public StockAmmoPool ammoNotInClip
        {
            get
            {
                m_ammoNotInClip.ammoBase = (clientBase + (int)Addresses.m_ammoNotInClip);
                return m_ammoNotInClip;
            }
        }
        public float[] _origin
        {
            set
            {
                memory.WriteFloat(clientBase + (int)Addresses.m_origin + 0, value[0]);
                memory.WriteFloat(clientBase + (int)Addresses.m_origin + 4, value[1]);
                memory.WriteFloat(clientBase + (int)Addresses.m_origin + 8, value[2]);
            }
        }
        public Vector origin
        {
            get
            {
                m_origin.vecBase = clientBase + (int)Addresses.m_origin;
                return m_origin;
            }
        }
        public Vector velocity
        {
            get
            {
                m_velocity.vecBase = clientBase + (int)Addresses.m_velocity;
                return m_velocity;
            }
        }
        #endregion
    }
}
