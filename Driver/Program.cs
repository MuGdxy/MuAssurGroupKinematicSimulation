using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAGKS;
namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector a = new Vector(0, 0);
            Vector b = a;
            b.x = 1;
        }
    }
}
