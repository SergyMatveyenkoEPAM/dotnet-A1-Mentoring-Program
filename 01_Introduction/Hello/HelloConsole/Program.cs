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
                System.Console.WriteLine($"Hello, {name}!");
            }
        }
    }
}
