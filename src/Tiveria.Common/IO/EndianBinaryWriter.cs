using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tiveria.Common.IO
{
    /// <summary>
    /// Endian aware binary writer.
    /// </summary>
    public class EndianBinaryWriter : BinaryWriter
    {
        #region Private Members

        private bool _swapBytes = false;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Initializes an instance of the <see cref="EndianBinaryWriter"/> class.
        /// </summary>
        /// <param name="output">Stream to which output should be written.</param>
        /// <param name="endian">Endianness of the output.</param>
        public EndianBinaryWriter(Stream output, Endian endian = Endian.Little)
            : base(output)
        {
            Endian = endian;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="EndianBinaryWriter"/> class.
        /// </summary>
        /// <param name="output">Stream to which output should be written.</param>
        /// <param name="encoding">Output encoding.</param>
        /// <param name="endian">Endianness of the output.</param>
        public EndianBinaryWriter(Stream output, Encoding encoding, Endian endian = Endian.Little)
            : base(output, encoding)
        {
            Endian = endian;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the endianness of the binary writer.
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
                    _swapBytes = Endian.Big == value;
                }
                else
                {
                    _swapBytes = Endian.Little == value;
                }
            }
        }

        #endregion

        #region Private Methods

        private void WriteInternal(byte[] buffer)
        {
            if (_swapBytes)
            {
                Array.Reverse(buffer);
            }
            base.Write(buffer);
        }

        #endregion

        #region BinaryWriter Overrides

        /// <inheritdoc />
        public override void Write(double value)
        {
            if (_swapBytes)
            {
                var b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(float value)
        {
            if (_swapBytes)
            {
                var b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(int value)
        {
            if (_swapBytes)
            {
                var b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(long value)
        {
            if (_swapBytes)
            {
                var b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(short value)
        {
            if (_swapBytes)
            {
                var b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(uint value)
        {
            if (_swapBytes)
            {
                byte[] b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(ulong value)
        {
            if (_swapBytes)
            {
                byte[] b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <inheritdoc />
        public override void Write(ushort value)
        {
            if (_swapBytes)
            {
                byte[] b = BitConverter.GetBytes(value);
                WriteInternal(b);
            }
            else
            {
                base.Write(value);
            }
        }
        #endregion
    }
}
