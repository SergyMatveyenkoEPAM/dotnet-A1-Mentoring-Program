using FibonacciNumbersLibrary;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FibonacciNumbersApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var fibonacciNumbersManager = new FibonacciNumbersManager(new FibonacciNumbersMemoryCache());
            NumbersOutput(fibonacciNumbersManager.GetFibonacciNumbers(5));
            NumbersOutput(fibonacciNumbersManager.GetFibonacciNumbers(10));
            NumbersOutput(fibonacciNumbersManager.GetFibonacciNumbers(15));

        }

        static void NumbersOutput(IEnumerable<int> numbers)
        {
            foreach (var number in numbers)
            {
                Console.Write(number + ", ");
            }

            Console.WriteLine("\n----------------------------------------");
        }
    }
}
