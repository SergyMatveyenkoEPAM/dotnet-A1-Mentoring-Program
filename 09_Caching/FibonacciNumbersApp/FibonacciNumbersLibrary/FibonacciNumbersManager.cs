using System.Collections.Generic;

namespace FibonacciNumbersLibrary
{
    public class FibonacciNumbersManager
    {
        private readonly IFibonacciCache _cache;

        public FibonacciNumbersManager(IFibonacciCache cache)
        {
            _cache = cache;
        }

        public IEnumerable<int> GetFibonacciNumbers(int length)
        {
            var list = new List<int>();

            int? number;

            for (int i = 0; i < length; i++)
            {
                number = _cache.GetFibonacciNumber(i);
                if (number == null)
                {
                    number = FibonacciNumbersCalculator.GetFibonacciNumber(i);
                    _cache.SetFibonacciNumber(i, (int)number);
                }

                list.Add((int)number);
            }

            return list;
        }
    }
}
