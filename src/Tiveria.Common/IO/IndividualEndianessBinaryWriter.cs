using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tiveria.Common.IO
{
    /// <summary>
    /// A <see cref="BinaryWriter"/> based class that allows specifying endianess on a individual write level.
    /// </summary>
    public class IndividualEndianessBinaryWriter : BinaryWriter
    {
         public override void Write(byte[] buffer)
        {
            base.Write(buffer);
        }

        public override void Write(byte[] buffer, int index, int count)
        {
            base.Write(buffer, index, count);
        }

        public override void Write(char ch)
        {
            base.Write(ch);
        }

        public override void Write(char[] chars)
        {
            base.Write(chars);
        }

        public override void Write(char[] chars, int index, int count)
        {
            base.Write(chars, index, count);
        }

        public override void Write(decimal value)
        {
            base.Write(value);
        }

        public override void Write(double value)
        {
            base.Write(value);
        }

        public override void Write(short value)
        {
            base.Write(value);
        }

        public override void Write(int value)
        {
            base.Write(value);
        }

        public override void Write(long value)
        {
            base.Write(value);
        }

        public override void Write(sbyte value)
        {
            base.Write(value);
        }

        public override void Write(float value)
        {
            base.Write(value);
        }

        public override void Write(string value)
        {
            base.Write(value);
        }

        public override void Write(ushort value)
        {
            base.Write(value);
        }

        public override void Write(uint value)
        {
            base.Write(value);
        }

        public override void Write(ulong value)
        {
            base.Write(value);
        }
    }
}
