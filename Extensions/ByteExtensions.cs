using System;

namespace Tiveria.Common.Extensions
{
    public static class ByteExtensions
    {
        public static bool IsBitSet(this byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public static bool IsBitSet(this int b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public static string ToHexString(this byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        public static void Slice(this byte[] source, byte[] destination, int srcindex, int destindex, int length)
        {
            if (srcindex + length > source.Length)
                throw new ArgumentOutOfRangeException("Source array too small");
            if (destindex + length > destination.Length)
                throw new ArgumentOutOfRangeException("Destination array too small");

            //for (var i = 0; i < length; i++)
            //    destination[destindex + i] = source[srcindex + i];
            Array.Copy(source, srcindex, destination, destindex, length);
        }

    }
}
