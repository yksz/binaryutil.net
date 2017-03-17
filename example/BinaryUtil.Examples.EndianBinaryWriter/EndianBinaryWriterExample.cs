using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryUtil.Examples
{
    class EndianBinaryWriterExample
    {
        public static void Main(string[] args)
        {
            ByteOrder endian = ByteOrder.BigEndian;

            // write
            byte[] buf = new byte[32];
            byte[] bytes1;
            using (var w = new EndianBinaryWriter(new MemoryStream(buf), endian))
            {
                var str1 = "abcd";
                bytes1 = Encoding.ASCII.GetBytes(str1);
                w.Write(bytes1);
                w.Write(true);
                w.Write(1);
                w.Write(2);
                w.Write(3);
                w.Write(4);
                w.Write(5.0f);
                w.Write(6.0);
            }

            // read
            var r = new ByteBuffer(buf, endian);
            var bytes2 = r.Get(bytes1.Length);
            var str2 = Encoding.ASCII.GetString(bytes2);
            Console.WriteLine(str2);           // => "abcd"
            Console.WriteLine(r.GetBoolean()); // => True
            Console.WriteLine(r.Get());        // => 1
            Console.WriteLine(r.GetUInt16());  // => 2
            Console.WriteLine(r.GetUInt32());  // => 3
            Console.WriteLine(r.GetUInt64());  // => 4
            Console.WriteLine(r.GetSingle());  // => 5
            Console.WriteLine(r.GetDouble());  // => 6
        }
    }
}
