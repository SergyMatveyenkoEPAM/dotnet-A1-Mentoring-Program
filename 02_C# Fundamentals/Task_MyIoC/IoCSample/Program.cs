using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyIoC;

namespace IoCSample
{
    class Program
    {
        static void Main()
        {
            var container = new Container();

            container.AssemblySample();

            Console.WriteLine("**********************************");

            container.ExplicitSample();

            Console.ReadKey();
        }
    }
}
