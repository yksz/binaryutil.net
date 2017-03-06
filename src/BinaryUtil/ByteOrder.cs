using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryUtil
{
    public class ByteOrder
    {
        public static readonly ByteOrder LittleEndian = new ByteOrder("LittleEndian");
        public static readonly ByteOrder BigEndian = new ByteOrder("BigEndian");

        public static ByteOrder NativeOrder
        {
            get
            {
                return BitConverter.IsLittleEndian ? LittleEndian : BigEndian;
            }
        }

        private string _name;

        private ByteOrder(string name)
        {
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
