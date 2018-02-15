using System;
using System.Text;
using System.IO;
using Tiveria.Common.Extensions;

namespace Tiveria.Common.IO
{
    /// <summary>
    /// Endian aware binary reader.
    /// </summary>
    public class EndianBinaryReader : ExtendedBinaryReader
    {
        #region Private Members

        private bool _swapBytes = false;

        private byte[] _internalBuffer = new byte[8];

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="EndianBinaryReader"/> class.
        /// </summary>
        /// <param name="input">Stream from which to read.</param>
        /// <param name="endian">Endianness of the <paramref name="input"/>. Default values is <c>Endian.Little</c></param>
        public EndianBinaryReader(Stream input, Endian endian = Endian.Little)
            : base(input)
        {
            Endian = endian;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="EndianBinaryReader"/> class.
        /// </summary>
        /// <param name="input">Stream from which to read.</param>
        /// <param name="encoding">Encoding of the <paramref name="input"/>.</param>
        /// <param name="endian">Endianness of the <paramref name="input"/>. Default values is <c>Endian.Little</c></param>
        public EndianBinaryReader(Stream input, Encoding encoding, Endian endian = Endian.Little)
            : base(input, encoding)
        {
            Endian = endian;
        }

        ///<summary>
        /// Creates a BinaryReader backed by a file (RO)
        ///</summary>
        /// <param name="file"></param>
        /// <param name="endian"></param>
        public EndianBinaryReader(string file, Endian endian = Endian.Little) : base(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            Endian = endian;
        }


        ///<summary>
        ///Creates a BinaryReader backed by a byte buffer
        ///</summary>
        public EndianBinaryReader(byte[] bytes, Endian endian = Endian.Little) : base(new MemoryStream(bytes))
        {
            Endian = endian;
        }

        ///<summary>
        ///Creates a BinaryReader backed by a byte buffer
        ///</summary>
        public EndianBinaryReader(byte[] bytes, int offset, Endian endian = Endian.Little) : base(new MemoryStream(bytes, offset, bytes.Length - offset))
        {
            Endian = endian;
        }

        ///<summary>
        ///Creates a BinaryReader backed by a byte buffer
        ///</summary>
        public EndianBinaryReader(byte[] bytes, int offset, int count, Endian endian = Endian.Little) : base(new MemoryStream(bytes, offset, count))
        {
            Endian = endian;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the endianness of the binary reader.
        /// </summary>
        public Endian Endian
        {
            get
            {
                if (BitConverter.IsLittleEndian)
                {
                    return _swapBytes ? Endian.Big : Endian.Little;
                }
                else
                {
                    return _swapBytes ? Endian.Little : Endian.Big;
                }
            }
            protected set
            {
                if (BitConverter.IsLittleEndian)
                {
                    _swapBytes = (Endian.Big == value);
                }
                else
                {
                    _swapBytes = (Endian.Little == value);
                }
            }
        }

        public bool UseInternalBuffer
        {
            get
            {
                return _internalBuffer != null;
            }
            set
            {
                if (value && (_internalBuffer == null))
                {
                    _internalBuffer = new byte[8];
                }
                else
                {
                    _internalBuffer = null;
                }
            }
        }

        #endregion

        #region Private Methods

        private byte[] ReadBytesInternal(int count)
        {
            byte[] buffer = null;
            if (_internalBuffer != null)
            {
                base.Read(_internalBuffer, 0, count);
                buffer = _internalBuffer;
            }
            else
            {
                buffer = base.ReadBytes(count);
            }
            if (_swapBytes)
            {
                Array.Reverse(buffer, 0, count);
            }
            return buffer;
        }

        #endregion

        #region BinaryReader Overrides

        /// <inheritdoc />
        public override short ReadInt16()
        {
            if (_swapBytes)
            {
                return base.ReadInt16().Swap();
            }
            return base.ReadInt16();
        }

        /// <inheritdoc />
        public override int ReadInt32()
        {
            if (_swapBytes)
            {
                return base.ReadInt32().Swap();
            }
            return base.ReadInt32();
        }

        /// <inheritdoc />
        public override long ReadInt64()
        {
            if (_swapBytes)
            {
                return base.ReadInt64().Swap();
            }
            return base.ReadInt64();
        }

        /// <inheritdoc />
        public override float ReadSingle()
        {
            if (_swapBytes)
            {
                byte[] b = ReadBytesInternal(4);
                return BitConverter.ToSingle(b, 0);
            }
            return base.ReadSingle();
        }

        /// <inheritdoc />
        public override double ReadDouble()
        {
            if (_swapBytes)
            {
                byte[] b = ReadBytesInternal(8);
                return BitConverter.ToDouble(b, 0);
            }
            return base.ReadDouble();
        }

        /// <inheritdoc />
        public override ushort ReadUInt16()
        {
            if (_swapBytes)
            {
                return base.ReadUInt16().Swap();
            }
            return base.ReadUInt16();
        }

        /// <inheritdoc />
        public override uint ReadUInt32()
        {
            if (_swapBytes)
            {
                return base.ReadUInt32().Swap();
            }
            return base.ReadUInt32();
        }

        /// <inheritdoc />
        public override ulong ReadUInt64()
        {
            if (_swapBytes)
            {
                return base.ReadUInt64().Swap();
            }
            return base.ReadUInt64();
        }
        #endregion
    }
}