using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Globalization;

public class Memory
{
    [DllImport("kernelbase.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out int lpNumberOfBytesWritten);

    [DllImport("kernelbase.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] lpBuffer, UInt32 size, out int lpNumberOfBytesWritten);

    [DllImport("kernelbase.dll")]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    [DllImport("kernelbase.dll")]
    private static extern IntPtr GetCurrentProcess();

    [DllImport("kernelbase.dll")]
    private static extern IntPtr GetModuleHandleA(string lpModuleName);

    public Process CurrentProcess;

    private bool CheckPattern(string pattern, byte[] array2check)
    {
        int len = array2check.Length;
        string[] strBytes = pattern.Split(' ');
        int x = 0;
        foreach (byte b in array2check)
        {
            if (strBytes[x] == "?" || strBytes[x] == "??")
            {
                x++;
            }
            else if (byte.Parse(strBytes[x], NumberStyles.HexNumber) == b)
            {
                x++;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public Int64 FindBinary(string module, string pattern)
    {
        ProcessModule pMod = GetModuleInfo(module);
        IntPtr baseAddy = pMod.BaseAddress;
        uint dwSize = (uint)pMod.ModuleMemorySize;
        byte[] memDump = ReadBytes(baseAddy.ToInt64(), dwSize);
        string[] pBytes = pattern.Split(' ');
        try
        {
            for (int y = 0; y < memDump.Length; y++)
            {
                if (memDump[y] == byte.Parse(pBytes[0], NumberStyles.HexNumber))
                {
                    byte[] checkArray = new byte[pBytes.Length];
                    for (int x = 0; x < pBytes.Length; x++)
                    {
                        checkArray[x] = memDump[y + x];
                    }
                    if (CheckPattern(pattern, checkArray))
                    {
                        return baseAddy.ToInt64() + y;
                    }
                    else
                    {
                        y += pBytes.Length - (pBytes.Length / 2);
                    }
                }
            }
        }
        catch (Exception)
        {
            return -1;
        }
        return -1;
    }

    public byte[] ReadBytes(Int64 pAddress, uint size)
    {
        byte[] bytes = new byte[size];

        ReadProcessMemory(CurrentProcess.Handle, (IntPtr)pAddress, bytes, size, out int size_out);

        return bytes;
    }

    public void WriteBytes(Int64 pAddress, byte[] value)
    {
        WriteProcessMemory(CurrentProcess.Handle, (IntPtr)pAddress, value, (uint)value.Length, out int test);
    }

    public Int64 GetModuleAddress(string module)
    {
        foreach (ProcessModule procModule in CurrentProcess.Modules)
        {
            if (procModule.ModuleName.ToLower() == module.ToLower())
            {
                return procModule.BaseAddress.ToInt64();
            }
        }
        return 0;
    }

    public ProcessModule GetModuleInfo(string module)
    {
        foreach (ProcessModule procModule in CurrentProcess.Modules)
        {
            if (procModule.ModuleName.ToLower() == module.ToLower())
            {
                return procModule;
            }
        }
        return null;
    }

    public Int64 ReadPointer(Int64 pAddress)
    {
        byte[] bytes = ReadBytes(pAddress, 8);
        return BitConverter.ToInt64(bytes, 0);
    }

    public byte ReadByte(Int64 pAddress)
    {
        byte[] bytes = ReadBytes(pAddress, 1);
        return bytes[0];
    }

    public int ReadInt(Int64 pAddress)
    {
        byte[] bytes = ReadBytes(pAddress, 4);
        return BitConverter.ToInt32(bytes, 0);
    }

    public uint ReadUInt(Int64 pAddress)
    {
        byte[] bytes = ReadBytes(pAddress, 4);
        return BitConverter.ToUInt32(bytes, 0);
    }
    public float ReadFloat(Int64 pAddress)
    {
        byte[] bytes = ReadBytes(pAddress, 4);
        return BitConverter.ToSingle(bytes, 0);
    }

    public void WriteInt(Int64 pAddress, int value)
    {
        WriteBytes(pAddress, BitConverter.GetBytes(value));
    }

    public void WriteUInt(Int64 pAddress, uint value)
    {
        WriteBytes(pAddress, BitConverter.GetBytes(value));
    }

    public void WriteByte(Int64 pAddress, byte value)
    {
        WriteBytes(pAddress, BitConverter.GetBytes(value));
    }

    public void WritePointer(Int64 pAddress, Int64 value)
    {
        WriteBytes(pAddress, BitConverter.GetBytes(value));
    }

    public void WriteFloat(Int64 pAddress, float value)
    {
        WriteBytes(pAddress, BitConverter.GetBytes(value));
    }

    public string ReadString(Int64 offset)
    {
        uint blocksize = 40;
        uint scalesize = 0;
        string str = string.Empty;

        while (!str.Contains('\0'))
        {
            byte[] buffer = ReadBytes((Int64)(offset + scalesize), blocksize);
            str += Encoding.UTF8.GetString(buffer);
            scalesize += blocksize;
        }

        return str.Substring(0, str.IndexOf('\0'));
    }

    public bool IsProcessRunning(string process)
    {
        try
        {
            CurrentProcess = Process.GetProcessesByName(process)[0];
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        return false;
    }

    public bool IsSafe()
    {
        return true;
        byte[] kernel32_wpm_o = new byte[6] { 0x48, 0xFF, 0x25, 0xE1, 0x5B, 0x04 };
        byte[] kernel32_rpm_o = new byte[6] { 0x48, 0xFF, 0x25, 0x41, 0x63, 0x06 };

        byte[] kernel32_wpm = new byte[6];
        byte[] kernel32_rpm = new byte[6];

        byte[] kernelbase_wpm_o = new byte[6] { 0x48, 0x8B, 0xC4, 0x48, 0x89, 0x58 };
        byte[] kernelbase_rpm_o = new byte[6] { 0x48, 0x83, 0xEC, 0x48, 0x48, 0x8D };

        byte[] kernelbase_wpm = new byte[6];
        byte[] kernelbase_rpm = new byte[6];

        byte[] ntdll_ntwpm_o = new byte[6] { 0x4C, 0x8B, 0xD1, 0xB8, 0x3A, 0x00 };
        byte[] ntdll_ntrpm_o = new byte[6] { 0x4C, 0x8B, 0xD1, 0xB8, 0x3F, 0x00 };

        byte[] ntdll_ntwpm = new byte[6];
        byte[] ntdll_ntrpm = new byte[6];


        IntPtr pWriteProcKernel32 = GetProcAddress(GetModuleHandleA("kernel32.dll"), "WriteProcessMemory");
        IntPtr pReadProcKernel32 = GetProcAddress(GetModuleHandleA("kernel32.dll"), "ReadProcessMemory");

        IntPtr pWriteProcKernelBase = GetProcAddress(GetModuleHandleA("kernelbase.dll"), "WriteProcessMemory");
        IntPtr pReadProcKernelBase = GetProcAddress(GetModuleHandleA("kernelbase.dll"), "ReadProcessMemory");

        IntPtr pWriteProcNTDll = GetProcAddress(GetModuleHandleA("ntdll.dll"), "NtWriteVirtualMemory");
        IntPtr pReadProcNtDll = GetProcAddress(GetModuleHandleA("ntdll.dll"), "NtReadVirtualMemory");

        //System.Windows.Forms.Clipboard.SetText($"{pReadProcNtDll:X}");
        int outint;

        if (ReadProcessMemory(GetCurrentProcess(), pWriteProcKernel32, kernel32_wpm, 6, out outint) && ReadProcessMemory(GetCurrentProcess(), pReadProcKernel32, kernel32_rpm, 6, out outint) &&
            ReadProcessMemory(GetCurrentProcess(), pWriteProcKernelBase, kernelbase_wpm, 6, out outint) && ReadProcessMemory(GetCurrentProcess(), pReadProcKernelBase, kernelbase_rpm, 6, out outint) &&
            ReadProcessMemory(GetCurrentProcess(), pWriteProcNTDll, ntdll_ntwpm, 6, out outint) && ReadProcessMemory(GetCurrentProcess(), pReadProcNtDll, ntdll_ntrpm, 6, out outint))
        {
            for (int i = 0; i < kernel32_wpm_o.Length; i++)
            {
                if (kernel32_wpm[i] != kernel32_wpm_o[i])
                    return false;
            }

            for (int i = 0; i < kernel32_rpm_o.Length; i++)
            {
                if (kernel32_rpm[i] != kernel32_rpm_o[i])
                    return false;
            }

            for (int i = 0; i < kernelbase_wpm_o.Length; i++)
            {
                if (kernelbase_wpm[i] != kernelbase_wpm_o[i])
                    return false;
            }

            for (int i = 0; i < kernelbase_rpm_o.Length; i++)
            {
                if (kernelbase_rpm[i] != kernelbase_rpm_o[i])
                    return false;
            }

            for (int i = 0; i < ntdll_ntwpm_o.Length; i++)
            {
                if (ntdll_ntwpm[i] != ntdll_ntwpm_o[i])
                    return false;
            }

            for (int i = 0; i < ntdll_ntrpm_o.Length; i++)
            {
                if (ntdll_ntrpm[i] != ntdll_ntrpm_o[i])
                    return false;
            }

            return true;
        }

        return true;
    }

}