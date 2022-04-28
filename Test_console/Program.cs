using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_console
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 1;
            byte[] c = BitConverter.GetBytes(a);
            Console.WriteLine(c.Length);
            Console.ReadKey();
        }
    }
}
