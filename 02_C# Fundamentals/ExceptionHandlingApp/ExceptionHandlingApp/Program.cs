using ExceptionHandlingLibrary;
using System;

namespace ExceptionHandlingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputTheFirstSymbolOfTheString();

            Console.WriteLine("--------------------------------\n");

            ConvertStringToInteger();
        }

        static void OutputTheFirstSymbolOfTheString()
        {
            Console.WriteLine("Continue input strings, to show you finished enter \"stop\"");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "stop")
                {
                    break;
                }

                try
                {
                    Console.WriteLine(input[0]);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void ConvertStringToInteger()
        {
            string stringWithLetters = "235dfhj";
            try
            {
                Console.WriteLine(ConverterToInt32.GetInt32(stringWithLetters));
            }
            catch (ConvertException e)
            {
                Console.WriteLine(e.Message);
            }

            string stringWithTooBigNumber = "23533333333333333333333333333333333333333333";
            try
            {
                Console.WriteLine(ConverterToInt32.GetInt32(stringWithTooBigNumber));
            }
            catch (ConvertException e)
            {
                Console.WriteLine(e.Message);
            }

            string stringWithCorrectNumber = "2353333";
            try
            {
                Console.WriteLine(ConverterToInt32.GetInt32(stringWithCorrectNumber));
            }
            catch (ConvertException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
