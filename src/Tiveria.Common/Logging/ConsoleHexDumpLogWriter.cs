using System;

namespace Tiveria.Common.Logging
{
    public class ConsoleHexDumpLogWriter : IHexDumpLogWriter
    {
        public void WriteLine(string line)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} [HexDump] {line}");
        }
    }
}
