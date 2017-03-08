using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryUtil
{
    public class EndianBinaryReader : BinaryReader
    {
        private readonly bool _shouldConvertEndian;

        public EndianBinaryReader(Stream input, ByteOrder order) : this(input, order, new UTF8Encoding()) { }

        public EndianBinaryReader(Stream input, ByteOrder order, Encoding encoding) : base(input, encoding)
        {
            if (order == null)
                throw new ArgumentNullException("order must not be null");

            _shouldConvertEndian = (ByteOrder.NativeOrder != order);
        }

        public override int Read()
        {
            return ReadChar();
        }

        public override char[] ReadChars(int count)
        {
            var chars = new char[count];
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = ReadChar();
            }
            return chars;
        }

        public override int Read(char[] buffer, int index, int count)
        {
            throw new NotSupportedException();
        }

        public override bool ReadBoolean()
        {
            return ReadValue(1, BitConverter.ToBoolean);
        }

        public override char ReadChar()
        {
            return ReadValue(2, BitConverter.ToChar);
        }

        public override short ReadInt16()
        {
            return ReadValue(2, BitConverter.ToInt16);
        }

        public override int ReadInt32()
        {
            return ReadValue(4, BitConverter.ToInt32);
        }

        public override long ReadInt64()
        {
            return ReadValue(8, BitConverter.ToInt64);
        }

        public override ushort ReadUInt16()
        {
            return ReadValue(2, BitConverter.ToUInt16);
        }

        public override uint ReadUInt32()
        {
            return ReadValue(4, BitConverter.ToUInt32);
        }

        public override ulong ReadUInt64()
        {
            return ReadValue(8, BitConverter.ToUInt64);
        }

        public override float ReadSingle()
        {
            return ReadValue(4, BitConverter.ToSingle);
        }

        public override double ReadDouble()
        {
            return ReadValue(8, BitConverter.ToDouble);
        }

        private T ReadValue<T>(int byteSize, Func<byte[], int, T> toType)
        {
            var bytes = base.ReadBytes(byteSize);
            if (_shouldConvertEndian)
            {
                Array.Reverse(bytes);
            }
            return toType(bytes, 0);
        }
    }
}