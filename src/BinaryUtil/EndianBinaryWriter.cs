using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryUtil
{
    public class EndianBinaryWriter : BinaryWriter
    {
        private readonly bool _shouldConvertEndian;

        public EndianBinaryWriter(Stream output, ByteOrder order) : this(output, order, new UTF8Encoding()) { }

        public EndianBinaryWriter(Stream output, ByteOrder order, Encoding encoding) : base(output, encoding)
        {
            if (order == null)
                throw new ArgumentNullException("order must not be null");

            _shouldConvertEndian = (ByteOrder.NativeOrder != order);
        }

        public override void Write(char[] chars)
        {
            foreach (char c in chars)
            {
                Write(c);
            }
        }

        public override void Write(char[] chars, int index, int count)
        {
            throw new NotSupportedException();
        }

        public override void Write(bool value)
        {
            WriteValue(value, BitConverter.GetBytes);
        }

        public override void Write(char value)
        {
            WriteValue(value, BitConverter.GetBytes);
        }

        public override void Write(short value)
        {
            WriteValue(value, BitConverter.GetBytes);
        }

        public override void Write(int value)
        {
            WriteValue(value, BitConverter.GetBytes);
        }

        public override void Write(long value)
        {
            WriteValue(value, BitConverter.GetBytes);
        }

        public override void Write(ushort value)
        {
            WriteValue(value, BitConverter.GetBytes);
        }

        public override void Write(uint value)
        {
            WriteValue(value, BitConverter.GetBytes);
        }

        public override void Write(ulong value)
        {
            WriteValue(value, BitConverter.GetBytes);
        }

        public override void Write(float value)
        {
            WriteValue(value, BitConverter.GetBytes);
        }

        public override void Write(double value)
        {
            WriteValue(value, BitConverter.GetBytes);
        }

        private void WriteValue<T>(T value, Func<T, byte[]> getBytes)
        {
            var bytes = getBytes(value);
            if (_shouldConvertEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }
    }
}
