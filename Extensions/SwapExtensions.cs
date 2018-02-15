using System;
using System.Threading.Tasks;

namespace Tiveria.Common.Extensions
{
    /// <summary>
    /// Extensions for multiple types that ease swapping byte order.
    /// Mainly used in the context of Endianess.
    /// </summary>
    public static class SwapExtensions
    {
        /// <summary>
        /// Swap byte order in <see cref="short"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static short Swap(this short value)
        {
            return (short)Swap((ushort)value);
        }

        /// <summary>
        /// Swap byte order in <see cref="ushort"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static ushort Swap(this ushort value)
        {
            return unchecked((ushort)((value >> 8) | (value << 8)));
        }

        /// <summary>
        /// Swap byte order in <see cref="int"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static int Swap(this int value)
        {
            return (int)Swap((uint)value);
        }

        /// <summary>
        /// Swap byte order in <see cref="uint"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static uint Swap(this uint value)
        {
            return
                unchecked(
                    ((value & 0x000000ffU) << 24) | ((value & 0x0000ff00U) << 8) | ((value & 0x00ff0000U) >> 8)
                    | ((value & 0xff000000U) >> 24));
        }

        /// <summary>
        /// Swap byte order in <see cref="long"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static long Swap(this long value)
        {
            return (long)Swap((ulong)value);
        }

        /// <summary>
        /// Swap byte order in <see cref="ulong"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static ulong Swap(this ulong value)
        {
            return
                unchecked(
                    ((value & 0x00000000000000ffU) << 56) | ((value & 0x000000000000ff00U) << 40)
                    | ((value & 0x0000000000ff0000U) << 24) | ((value & 0x00000000ff000000U) << 8)
                    | ((value & 0x000000ff00000000U) >> 8) | ((value & 0x0000ff0000000000U) >> 24)
                    | ((value & 0x00ff000000000000U) >> 40) | ((value & 0xff00000000000000U) >> 56));
        }

        /// <summary>
        /// Swap byte order in <see cref="float"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static float Swap(this float value)
        {
            var b = BitConverter.GetBytes(value);
            Array.Reverse(b);
            return BitConverter.ToSingle(b, 0);
        }

        /// <summary>
        /// Swap byte order in <see cref="double"/> value.
        /// </summary>
        /// <param name="value">Value in which bytes should be swapped.</param>
        /// <returns>Byte order swapped value.</returns>
        public static double Swap(this double value)
        {
            var b = BitConverter.GetBytes(value);
            Array.Reverse(b);
            return BitConverter.ToDouble(b, 0);
        }

        /// <summary>
        /// Swap byte order in array of <see cref="short"/> values.
        /// </summary>
        /// <param name="values">Array of <see cref="short"/> values.</param>
        public static void Swap(this short[] values)
        {
            Parallel.For(0, values.Length, i => { values[i] = Swap(values[i]); });
        }

        /// <summary>
        /// Swap byte order in array of <see cref="ushort"/> values.
        /// </summary>
        /// <param name="values">Array of <see cref="ushort"/> values.</param>
        public static void Swap(this ushort[] values)
        {
            Parallel.For(0, values.Length, i => { values[i] = Swap(values[i]); });
        }

        /// <summary>
        /// Swap byte order in array of <see cref="int"/> values.
        /// </summary>
        /// <param name="values">Array of <see cref="int"/> values.</param>
        public static void Swap(this int[] values)
        {
            Parallel.For(0, values.Length, i => { values[i] = Swap(values[i]); });
        }

        /// <summary>
        /// Swap byte order in array of <see cref="uint"/> values.
        /// </summary>
        /// <param name="values">Array of <see cref="uint"/> values.</param>
        public static void Swap(this uint[] values)
        {
            Parallel.For(0, values.Length, i => { values[i] = Swap(values[i]); });
        }
        /// <summary>
        /// Swap bytes in sequences of <paramref name="bytesToSwap"/>.
        /// </summary>
        /// <param name="bytesToSwap">Number of bytes to swap.</param>
        /// <param name="bytes">Array of bytes.</param>
        public static byte[] SwapBytes(this byte[] bytes, int bytesToSwap)
        {
            switch(bytesToSwap)
            {
                case 4: SwapBytes4(bytes);
                        break;
                case 2: SwapBytes2(bytes);
                        break;
                case 1: break;
                default:
                    SwapBytesInternally();
                    break;
            }
            return bytes;

            void SwapBytesInternally()
            {
                unchecked
                {
                    var l = bytes.Length - (bytes.Length % bytesToSwap);
                    for (var i = 0; i < l; i += bytesToSwap)
                    {
                        Array.Reverse(bytes, i, bytesToSwap);
                    }
                }
            }
        }

        /// <summary>
        /// Swap bytes in sequences of 2.
        /// </summary>
        /// <param name="bytes">Array of bytes.</param>
        public static byte[] SwapBytes2(this byte[] bytes)
        {
            unchecked
            {
                var l = bytes.Length - bytes.Length % 2;
                for (var i = 0; i < l; i += 2)
                {
                    var b = bytes[i + 1];
                    bytes[i + 1] = bytes[i];
                    bytes[i] = b;
                }
            }
            return bytes;
        }

        /// <summary>
        /// Swap bytes in sequences of 4.
        /// </summary>
        /// <param name="bytes">Array of bytes.</param>
        public static byte[] SwapBytes4(this byte[] bytes)
        {
            unchecked
            {
                var l = bytes.Length - (bytes.Length % 4);
                for (var i = 0; i < l; i += 4)
                {
                    var b = bytes[i + 3];
                    bytes[i + 3] = bytes[i];
                    bytes[i] = b;
                    b = bytes[i + 2];
                    bytes[i + 2] = bytes[i + 1];
                    bytes[i + 1] = b;
                }
            }
            return bytes;
        }

        /// <summary>
        /// Swap byte order in array of values.
        /// </summary>
        /// <typeparam name="T">Array element type, must be one of <see cref="short"/>, <see cref="ushort"/>, <see cref="int"/> or <see cref="uint"/>.</typeparam>
        /// <param name="values">Array of values to swap.</param>
        /// <exception cref="InvalidOperationException">if array element type is not <see cref="short"/>, <see cref="ushort"/>, <see cref="int"/> or <see cref="uint"/>.</exception>
        public static void Swap<T>(T[] values)
        {
            if (typeof(T) == typeof(short)) Swap(values as short[]);
            else if (typeof(T) == typeof(ushort)) Swap(values as ushort[]);
            else if (typeof(T) == typeof(int)) Swap(values as int[]);
            else if (typeof(T) == typeof(uint)) Swap(values as uint[]);
            else throw new InvalidOperationException("Attempted to byte swap non-specialized type: " + typeof(T).Name);
        }
    }
}