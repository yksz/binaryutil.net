using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryUtil.Examples
{
    class ByteBufferExample
    {
        public static void Main(string[] args)
        {
            // write
            ByteBuffer w = new ByteBuffer(32, ByteOrder.BigEndian);
            string str1 = "abcd";
            byte[] bytes1 = Encoding.ASCII.GetBytes(str1);
            w.Put(bytes1);
            w.PutBoolean(true);
            w.Put(1);
            w.PutUInt16(2);
            w.PutUInt32(3);
            w.PutUInt64(4);
            w.PutSingle(5.0f);
            w.PutDouble(6.0);

            // read
            ByteBuffer r = new ByteBuffer(w.Buffer, ByteOrder.BigEndian);
            byte[] bytes2 = new byte[4];
            r.Get(ref bytes2);
            string str2 = Encoding.ASCII.GetString(bytes2);
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
