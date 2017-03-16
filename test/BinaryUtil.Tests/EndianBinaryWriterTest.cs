using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BinaryUtil.Tests
{
    [TestClass]
    public class EndianBinaryWriterTest
    {
        [TestMethod]
        public void WriteInt32_LittleEndian()
        {
            WriteInt32(ByteOrder.LittleEndian);
        }

        [TestMethod]
        public void WriteInt32_BigEndian()
        {
            WriteInt32(ByteOrder.BigEndian);
        }

        public void WriteInt32(ByteOrder endian)
        {
            // setup:
            const int n = 123;
            byte[] buf = new byte[4];

            // when:
            using (var w = new EndianBinaryWriter(new MemoryStream(buf), endian))
            {
                w.Write(n);
            }

            // then:
            var r = new ByteBuffer(buf, endian);
            Assert.AreEqual(n, r.GetInt32());
        }

        [TestMethod]
        public void WriteChar_LittleEndian()
        {
            WriteChar(ByteOrder.LittleEndian);
        }

        [TestMethod]
        public void WriteChar_BigEndian()
        {
            WriteChar(ByteOrder.BigEndian);
        }

        public void WriteChar(ByteOrder endian)
        {
            // setup:
            const char c = '\u00A5';
            byte[] buf = new byte[2];

            // when:
            using (var w = new EndianBinaryWriter(new MemoryStream(buf), endian))
            {
                w.Write(c);
            }

            // then:
            var r = new ByteBuffer(buf, endian);
            Assert.AreEqual(c, r.GetChar());
        }
    }
}
