using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t8_cheat
{
    class XAssetPool
    {
        private Memory memory = null;

        public XAssetPool(Memory mem)
        {
            memory = mem;
        }

        private Int64 s_AssetPool;

        enum Addresses
        {
            m_assetPools = 0x912B5B0,
        }

        struct _XAssetPool
        {
            Int64 pool;
            uint itemSize;
            int itemCount;
            bool isSingleton;
            int itemAllocCount;
            Int64 freeHead;
        };

        public XAssetPool this[int index]
        {
            get
            {
                s_AssetPool = (memory.GetModuleAddress(Program.GameExecutableName) + (Int64)Addresses.m_assetPools) + (index * 0x20);
                return this;
            }
        }
        public Int64 pool
        {
            get
            {
                return memory.ReadPointer(s_AssetPool + 0);
            }
        }
        public uint itemSize
        {
            get
            {
                return memory.ReadUInt(s_AssetPool + 8);
            }
        }
        public int itemCount
        {
            get
            {
                return memory.ReadInt(s_AssetPool + 12);
            }
        }
        public int itemAllocCount
        {
            get
            {
                return memory.ReadInt(s_AssetPool + 16);
            }
        }


    }
}
