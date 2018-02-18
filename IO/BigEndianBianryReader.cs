using System.Text;
using System.IO;

namespace Tiveria.Common.IO
{
    /// <summary>
    /// 
    /// </summary>
    public class BigEndianBianryReader : EndianBinaryReader
    {
        public BigEndianBianryReader(Stream input) : base(input, Endian.Big)
        {
        }

        public BigEndianBianryReader(string file) : base(file, Endian.Big)
        {
        }

        public BigEndianBianryReader(byte[] bytes) : base(bytes, Endian.Big)
        {
        }

        public BigEndianBianryReader(Stream input, Encoding encoding) : base(input, encoding, Endian.Big)
        {
        }

        public BigEndianBianryReader(byte[] bytes, int offset) : base(bytes, offset, Endian.Big)
        {
        }

        public BigEndianBianryReader(byte[] bytes, int offset, int count) : base(bytes, offset, count, Endian.Big)
        {
        }
    }
}