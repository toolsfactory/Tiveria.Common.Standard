using System;
using System.IO;
using System.Text;

namespace Tiveria.Common.Logging
{
    public class HexDumpLogger : IHexDumpLogger
    {
        private readonly IHexDumpLogWriter _logWriter;
        private readonly byte[] _linebuffer = new byte[16];
        private int _bufferpos = 0;
        private HexDumpMode _currentMode = HexDumpMode.None;

        public HexDumpLogger(IHexDumpLogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        private void CheckNewMode(HexDumpMode mode)
        {
            if (mode != _currentMode)
            {
                Flush();
                if (mode == HexDumpMode.BytesOut)
                {
                    _logWriter.WriteLine(">>> Sending now"); 
                } 
                else 
                { 
                    _logWriter.WriteLine("<<< receiving now"); 
                }
                _currentMode = mode;
            }
        }

        private void CheckAutoFlush()
        {
            if (_bufferpos == 15) // 16th byte to show
                Flush();
        }

        public void DumpByte(HexDumpMode mode, byte data)
        {
            CheckNewMode(mode);
            _linebuffer[_bufferpos++] = data;
            CheckAutoFlush();
        }

        public void DumpBytes(HexDumpMode mode, byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Length == 0)
                return;
            CheckNewMode(mode);
            for(var i = 0; i< data.Length; i++)
            {
                _linebuffer[_bufferpos++] = data[i];
                CheckAutoFlush();
            }
            CheckAutoFlush();
        }

        public void DumpControlMessage(string message)
        {
            Flush();
            _logWriter.WriteLine("### " + message);
        }


        public void Flush()
        {
            StringBuilder strHex = new StringBuilder();
            StringBuilder strRaw = new StringBuilder();

            for (var i=0; i<16; i++)
            {
                if(i>_bufferpos)
                {
                    strHex.Append(".. ");
                    strRaw.Append(" ");
                }
                else
                {
                    byte b = _linebuffer[i];
                    strHex.AppendFormat("{0:x2} ", b);
                    if (b < 32)
                        strRaw.Append(" ");
                    else
                        strRaw.Append((char)b);
                }
            }
            _bufferpos = 0;
            var str = strHex.Append(" |  ").Append(strRaw).ToString();
            _logWriter.WriteLine(str);
        }
    }

    static class HexConverter
    {
        private static readonly Char[] hexChars;
        static HexConverter()
        {
            hexChars = "0123456789abcdef".ToCharArray();
        }

        static void Byte2HexInBuffer(char chr, ref byte[] buffer, int pos)
        {
            buffer[pos++] = (byte)hexChars[((byte)chr) & 0x0f];
            buffer[pos] = (byte)hexChars[((byte)chr) & 0x0f >> 4];
        }
    }
}
