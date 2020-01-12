using System;

namespace Tiveria.Common.Logging
{
    public enum HexDumpMode
    {
        None,
        BytesIn,
        BytesOut
    }
    public interface IHexDumpLogger
    {
        void DumpByte(HexDumpMode mode, byte data);
        void DumpBytes(HexDumpMode mode, byte[] data);
        void DumpControlMessage(string message);
        void Flush();
    }
}
