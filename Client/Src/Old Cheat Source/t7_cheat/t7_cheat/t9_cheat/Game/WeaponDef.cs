using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t9_cheat
{
    public class Defs
    {
        private Memory memory = null;
        public Int64 weapdef;
        public Defs(Memory mem)
        {
            memory = mem;
        }
        public void InitializeValue(Int64 pAddr)
        {
            weapdef = pAddr;
        }

        public uint weapAmmoType
        {
            get
            {
                return memory.ReadUInt(weapdef + 0xEFC);
            }
        }
        public int iClipSize
        {
            get
            {
                return memory.ReadInt(weapdef + 0x5AC); //most likely wrong
            }
        }
    }

    public class WeaponDef
    {
        private Memory memory = null;
        private Defs def;
        public WeaponDef(Memory mem)
        {
            memory = mem;
            def = new Defs(mem);
        }
        enum Addresses
        {
            m_weaponDefCount = 0x15FB56C0,
            m_weaponDef = 0x15FB56D0,
        }

        private Int64 weapDef;

        public int Length
        {
            get
            {
                Int64 baseAddr = memory.GetModuleAddress(Program.GameExecutableName);
                return memory.ReadInt(baseAddr + (Int64)Addresses.m_weaponDefCount) + 1;
            }
        }

        public WeaponDef this[int index]
        {
            get
            {
                Int64 baseAddr = memory.GetModuleAddress(Program.GameExecutableName);
                weapDef = memory.ReadPointer(baseAddr + (Int64)Addresses.m_weaponDef + (index * 8));
                def.InitializeValue(memory.ReadPointer(weapDef + 0xA38));
                return this;
            }
        }

        public Int64 szInternalName
        {
            get { return (memory.ReadPointer(weapDef)); }
        }

        public Defs defs
        {
            get
            {
                return def;
            }
        }

        public int GetIndexForHash(Int64 weapName)
        {
            for (int i = 0; i < this.Length; i++)
            {
                if (weapName == this[i].szInternalName)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
