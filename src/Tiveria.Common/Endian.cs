using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Tiveria.Common
{
    /// <summary>
    /// A simple Enum to ease working with endianess.
    /// </summary>
    public enum Endian
    {
        Big,
        Little
    }

    public static class EndianExtensions
    {
        public static Endian LocalMachine(this Endian endian)
        {
            return BitConverter.IsLittleEndian ? Endian.Little : Endian.Big;
        }

        public static Endian Network(this Endian endian)
        {
            return Endian.Big;
        }
    }
}
