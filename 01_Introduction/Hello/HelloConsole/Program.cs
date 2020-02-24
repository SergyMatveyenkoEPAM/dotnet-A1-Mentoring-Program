using HelloLibrary;
using System;
using System.Linq;

namespace HelloConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any())
            {
                string name = args[0];
                // Console.WriteLine($"Hello, {name}!");
                Console.WriteLine(Greeting.Hello(name));
            }
        }
    }
}
