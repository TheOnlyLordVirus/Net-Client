using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t7_cheat
{
    public class WeaponDef
    {
        private Memory memory = null;
        public WeaponDef(Memory mem)
        {
            memory = mem;
        }
        enum Addresses
        {
            m_weaponDef = 0x19C75290,
            m_weaponDefCount = 0x19C76294,
            m_invenotryType = 0x7C,
            m_iClipSize = 0x194,
        }

        private Int64 weapDef;

        public int Length
        {
            get
            {
                Int64 baseAddr = memory.CurrentProcess.MainModule.BaseAddress.ToInt64();
                return memory.ReadInt(baseAddr + (Int64)Addresses.m_weaponDefCount) + 1;
            }
        }

        public WeaponDef this[int index]
        {
            get
            {
                Int64 baseAddr = memory.CurrentProcess.MainModule.BaseAddress.ToInt64();
                weapDef = memory.ReadPointer(baseAddr + (Int64)Addresses.m_weaponDef + (index * 8));
                return this;
            }
        }

        public string szInternalName
        {
            get { return memory.ReadString(memory.ReadPointer(weapDef)); }
        }
        public int iClipSize
        {
            get { return memory.ReadInt(weapDef + (int)Addresses.m_iClipSize); }
            set { memory.WriteInt(weapDef + (int)Addresses.m_iClipSize, value); }
        }

        public int inventoryType
        {
            get { return memory.ReadInt(weapDef + (int)Addresses.m_invenotryType); }
        }


        public int GetIndexForName(string weapName)
        {
            for (int i = 0;i < this.Length;i++)
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
