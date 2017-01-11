using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryUtil.Tests
{
    [TestClass()]
    public class ByteBufferTests
    {
        [TestMethod()]
        public void GetInt32Test_LittleEndian()
        {
            var bytes = new byte[] { 0x01, 0x00, 0x00, 0x00 };
            var copy = new byte[bytes.Length];
            Array.Copy(bytes, copy, bytes.Length);

            var bb = new ByteBuffer(bytes, ByteOrder.LittleEndian);
            Assert.AreEqual(1, bb.GetInt32());
            CollectionAssert.AreEqual(copy, bytes);
        }

        [TestMethod()]
        public void GetInt32Test_BigEndian()
        {
            var bytes = new byte[] { 0x00, 0x00, 0x00, 0x01 };
            var copy = new byte[bytes.Length];
            Array.Copy(bytes, copy, bytes.Length);

            var bb = new ByteBuffer(bytes, ByteOrder.BigEndian);
            Assert.AreEqual(1, bb.GetInt32());
            CollectionAssert.AreEqual(copy, bytes);
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetInt32Test_Exception_OutOfRange()
        {
            var bytes = new byte[] { 0x00, 0x00, 0x00, 0x01 };
            var bb = new ByteBuffer(bytes, ByteOrder.BigEndian);
            bb.GetInt32();
            bb.GetInt32();
        }

        [TestMethod()]
        public void PutInt32Test_LittleEndian()
        {
            var bb = new ByteBuffer(4, ByteOrder.LittleEndian);
            bb.PutInt32(1);

            Assert.AreEqual(0x01, bb.Buffer[0]);
            Assert.AreEqual(0x00, bb.Buffer[1]);
            Assert.AreEqual(0x00, bb.Buffer[2]);
            Assert.AreEqual(0x00, bb.Buffer[3]);
        }

        [TestMethod()]
        public void PutInt32Test_BigEndian()
        {
            var bb = new ByteBuffer(4, ByteOrder.BigEndian);
            bb.PutInt32(1);

            Assert.AreEqual(0x00, bb.Buffer[0]);
            Assert.AreEqual(0x00, bb.Buffer[1]);
            Assert.AreEqual(0x00, bb.Buffer[2]);
            Assert.AreEqual(0x01, bb.Buffer[3]);
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void PutInt32Test_Exception_OutOfRange()
        {
            var bb = new ByteBuffer(4, ByteOrder.BigEndian);
            bb.PutInt32(1);
            bb.PutInt32(1);
        }
    }
}