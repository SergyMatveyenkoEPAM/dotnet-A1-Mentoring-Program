using System;
using System.Collections.Generic;

namespace FibonacciNumbersLibrary
{
    public class FibonacciNumbersCalculator
    {
        public static int GetFibonacciNumber(int numberPosition)
        {
            var list = new List<int>();

            list.Add(0);
            list.Add(1);

            for (int i = 1; list[i] < int.MaxValue / 2 && i < numberPosition; i++)
            {
                list.Add(list[i - 1] + list[i]);
            }

            return list[numberPosition];
        }
    }
}
