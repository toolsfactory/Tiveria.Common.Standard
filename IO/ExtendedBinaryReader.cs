using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tiveria.Common.IO
{
    /// <summary>
    /// A BinaryReader that supports additional functions.
    /// It's based on a <code>BinaryReader</code>
    /// </summary>
    public class ExtendedBinaryReader : System.IO.BinaryReader
    {
        #region public properties
        /// <summary>
        /// Check if the stream position is at the end of the stream
        /// </summary>
        public bool IsEof => BaseStream.Position >= BaseStream.Length;

        /// <summary>
        /// Get the current position in the stream
        /// </summary>
        public long Position => BaseStream.Position;

        /// <summary>
        /// Get the amount of bytes before eof
        /// </summary>
        public long Available => BaseStream.Length - BaseStream.Position;

        /// <summary>
        /// Get the total length of the stream
        /// </summary>
        public long Size => BaseStream.Length;
        #endregion

        #region constructors
        public ExtendedBinaryReader(Stream input) : base(input)
        {
        }

        public ExtendedBinaryReader(Stream input, Encoding encoding) : base(input, encoding)
        {
        }

        public ExtendedBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
        }

        ///<summary>
        /// Creates a ExtendedBinaryReader backed by a file (RO)
        ///</summary>
        public ExtendedBinaryReader(string file) : base(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
        { }

        ///<summary>
        ///Creates a ExtendedBinaryReader backed by a byte buffer
        ///</summary>
        public ExtendedBinaryReader(byte[] bytes) : base(new MemoryStream(bytes))
        { }

        ///<summary>
        ///Creates a ExtendedBinaryReader backed by a byte buffer
        ///</summary>
        public ExtendedBinaryReader(byte[] bytes, int offset) : base(new MemoryStream(bytes, offset, bytes.Length - offset))
        { }

        ///<summary>
        ///Creates a ExtendedBinaryReader backed by a byte buffer
        ///</summary>
        public ExtendedBinaryReader(byte[] bytes, int offset, int count) : base(new MemoryStream(bytes, offset, count))
        { }
        #endregion

        #region Stream positioning
        /// <summary>
        /// Seek to a specific position from the beginning of the stream
        /// </summary>
        /// <param name="position">The position to seek to</param>
        public void Seek(long position)
        {
            BaseStream.Seek(position, SeekOrigin.Begin);
        }
        #endregion

        #region Byte arrays
        /// <summary>
        /// Read a fixed number of bytes from the stream
        /// </summary>
        /// <param name="count">The number of bytes to read</param>
        /// <returns></returns>
        public byte[] ReadBytes(long count)
        {
            if (count < 0 || count > Int32.MaxValue)
                throw new ArgumentOutOfRangeException("requested " + count + " bytes, while only non-negative int32 amount of bytes possible");
            byte[] bytes = base.ReadBytes((int)count);
            if (bytes.Length < count)
                throw new EndOfStreamException("requested " + count + " bytes, but got only " + bytes.Length + " bytes");
            return bytes;
        }

        /// <summary>
        /// Read a fixed number of bytes from the stream
        /// </summary>
        /// <param name="count">The number of bytes to read</param>
        /// <returns></returns>
        public byte[] ReadBytes(ulong count)
        {
            if (count > Int32.MaxValue)
                throw new ArgumentOutOfRangeException("requested " + count + " bytes, while only non-negative int32 amount of bytes possible");
            int cnt = (int)count;
            byte[] bytes = base.ReadBytes(cnt);
            if (bytes.Length < cnt)
                throw new EndOfStreamException("requested " + count + " bytes, but got only " + bytes.Length + " bytes");
            return bytes;
        }

        /// <summary>
        /// Read all the remaining bytes from the stream until the end is reached
        /// </summary>
        /// <returns></returns>
        public byte[] ReadBytesFull()
        {
            return ReadBytes(BaseStream.Length - BaseStream.Position);
        }

        /// <summary>
        /// Read a terminated string from the stream
        /// </summary>
        /// <param name="terminator">The string terminator value</param>
        /// <param name="includeTerminator">True to include the terminator in the returned string</param>
        /// <param name="consumeTerminator">True to consume the terminator byte before returning</param>
        /// <param name="eosError">True to throw an error when the EOS was reached before the terminator</param>
        /// <returns></returns>
        public byte[] ReadBytesTerm(byte terminator, bool includeTerminator, bool consumeTerminator, bool eosError)
        {
            List<byte> bytes = new System.Collections.Generic.List<byte>();
            while (true)
            {
                if (IsEof)
                {
                    if (eosError) throw new EndOfStreamException(string.Format("End of stream reached, but no terminator `{0}` found", terminator));
                    break;
                }

                byte b = ReadByte();
                if (b == terminator)
                {
                    if (includeTerminator) bytes.Add(b);
                    if (!consumeTerminator) Seek(Position - 1);
                    break;
                }
                bytes.Add(b);
            }
            return bytes.ToArray();
        }
        #endregion
    }
}
