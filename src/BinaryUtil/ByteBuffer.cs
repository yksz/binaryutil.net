using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryUtil
{
    public class ByteBuffer
    {
        private readonly byte[] _buf;
        private readonly bool _shouldConvertEndian;
        private int _offset;

        public byte[] Buffer { get { return _buf; } }

        public ByteBuffer(int capacity, ByteOrder order) : this(order)
        {
            if (capacity < 1)
                throw new ArgumentException("capacity must not be less than 1");

            _buf = new byte[capacity];
        }

        public ByteBuffer(byte[] buf, ByteOrder order) : this(order)
        {
            if (buf == null)
                throw new ArgumentNullException("buf must not be null");

            _buf = (byte[])buf.Clone();
        }

        private ByteBuffer(ByteOrder order)
        {
            if (order == null)
                throw new ArgumentNullException("order must not be null");

            _shouldConvertEndian = (ByteOrder.NativeOrder != order);
        }

        public byte Get()
        {
            const int byteSize = 1;
            CheckOutOfRange(byteSize);
            var value = _buf[_offset];
            _offset += byteSize;
            return value;
        }

        public byte[] Get(int byteSize)
        {
            var b = new byte[byteSize];
            CheckOutOfRange(byteSize);
            Array.Copy(_buf, _offset, b, 0, byteSize);
            _offset += byteSize;
            return b;
        }

        public Boolean GetBoolean()
        {
            return GetValue(1, BitConverter.ToBoolean);
        }

        public Char GetChar()
        {
            return GetValue(2, BitConverter.ToChar);
        }

        public Int16 GetInt16()
        {
            return GetValue(2, BitConverter.ToInt16);
        }

        public Int32 GetInt32()
        {
            return GetValue(4, BitConverter.ToInt32);
        }

        public Int64 GetInt64()
        {
            return GetValue(8, BitConverter.ToInt32);
        }

        public UInt16 GetUInt16()
        {
            return GetValue(2, BitConverter.ToUInt16);
        }

        public UInt32 GetUInt32()
        {
            return GetValue(4, BitConverter.ToUInt32);
        }

        public UInt64 GetUInt64()
        {
            return GetValue(8, BitConverter.ToUInt32);
        }

        public Single GetSingle()
        {
            return GetValue(4, BitConverter.ToSingle);
        }

        public Double GetDouble()
        {
            return GetValue(8, BitConverter.ToDouble);
        }

        private T GetValue<T>(int byteSize, Func<byte[], int, T> toType)
        {
            CheckOutOfRange(byteSize);
            if (_shouldConvertEndian)
            {
                Array.Reverse(_buf, _offset, byteSize);
            }
            var value = toType(_buf, _offset);
            _offset += byteSize;
            return value;
        }

        public void Put(byte b)
        {
            const int byteSize = 1;
            CheckOutOfRange(byteSize);
            _buf[_offset] = b;
            _offset += byteSize;
        }

        public void Put(byte[] b)
        {
            int byteSize = b.Length;
            CheckOutOfRange(byteSize);
            Array.Copy(b, 0, _buf, _offset, byteSize);
            _offset += byteSize;
        }

        public void PutBoolean(Boolean value)
        {
            PutValue(1, value, BitConverter.GetBytes);
        }

        public void PutChar(Char value)
        {
            PutValue(2, value, BitConverter.GetBytes);
        }

        public void PutInt16(Int16 value)
        {
            PutValue(2, value, BitConverter.GetBytes);
        }

        public void PutInt32(Int32 value)
        {
            PutValue(4, value, BitConverter.GetBytes);
        }

        public void PutInt64(Int64 value)
        {
            PutValue(8, value, BitConverter.GetBytes);
        }

        public void PutUInt16(UInt16 value)
        {
            PutValue(2, value, BitConverter.GetBytes);
        }

        public void PutUInt32(UInt32 value)
        {
            PutValue(4, value, BitConverter.GetBytes);
        }

        public void PutUInt64(UInt64 value)
        {
            PutValue(8, value, BitConverter.GetBytes);
        }

        public void PutSingle(Single value)
        {
            PutValue(4, value, BitConverter.GetBytes);
        }

        public void PutDouble(Double value)
        {
            PutValue(8, value, BitConverter.GetBytes);
        }

        private void PutValue<T>(int byteSize, T value, Func<T, byte[]> getBytes)
        {
            CheckOutOfRange(byteSize);
            var bytes = getBytes(value);
            if (_shouldConvertEndian)
            {
                Array.Reverse(bytes, 0, byteSize);
            }
            Array.Copy(bytes, 0, _buf, _offset, byteSize);
            _offset += byteSize;
        }

        private void CheckOutOfRange(int byteSize)
        {
            if (_offset + byteSize > _buf.Length)
            {
                throw new IndexOutOfRangeException(
                        String.Format("capacity={0}, used={1}, using={2}", _buf.Length, _offset, byteSize));
            }
        }
    }
}
