using System.Text;
using System.IO;

namespace Tiveria.Common.IO
{
    public class BigEndianBinaryWriter : EndianBinaryWriter
    {
        public BigEndianBinaryWriter(Stream output) : base(output, Endian.Big)
        {
        }

        public BigEndianBinaryWriter(Stream output, Encoding encoding) : base(output, encoding, Endian.Big)
        {
        }
    }
}
