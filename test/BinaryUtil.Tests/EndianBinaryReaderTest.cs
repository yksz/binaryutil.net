using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BinaryUtil.Tests
{
    [TestClass]
    public class EndianBinaryReaderTest
    {
        [TestMethod]
        public void ReadInt32_LittleEndian()
        {
            ReadInt32(ByteOrder.LittleEndian);
        }

        [TestMethod]
        public void ReadInt32_BigEndian()
        {
            ReadInt32(ByteOrder.BigEndian);
        }

        private void ReadInt32(ByteOrder endian)
        {
            // setup:
            const int n = 123;

            // when:
            var w = new ByteBuffer(4, endian);
            w.PutInt32(n);

            // then:
            using (var r = new EndianBinaryReader(new MemoryStream(w.Buffer), endian))
            {
                Assert.AreEqual(n, r.ReadInt32());
            }
        }

        [TestMethod]
        public void ReadChar_LittleEndian()
        {
            ReadChar(ByteOrder.LittleEndian);
        }

        [TestMethod]
        public void ReadChar_BigEndian()
        {
            ReadChar(ByteOrder.BigEndian);
        }

        public void ReadChar(ByteOrder endian)
        {
            // setup:
            const char c = '\u00A5';

            // when:
            var w = new ByteBuffer(2, endian);
            w.PutChar(c);

            // then:
            using (var r = new EndianBinaryReader(new MemoryStream(w.Buffer), endian))
            {
                Assert.AreEqual(c, r.ReadChar());
            }
        }
    }
}
