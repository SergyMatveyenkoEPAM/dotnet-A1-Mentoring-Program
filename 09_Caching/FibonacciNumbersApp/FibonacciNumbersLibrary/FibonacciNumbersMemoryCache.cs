using System.Runtime.Caching;

namespace FibonacciNumbersLibrary
{
    public class FibonacciNumbersMemoryCache : IFibonacciCache
    {
        private ObjectCache _cache;

        public FibonacciNumbersMemoryCache()
        {
            _cache = MemoryCache.Default;
        }

        public int? GetFibonacciNumber(int numberPosition)
        {
            return (int?)_cache.Get(numberPosition.ToString());
        }

        public void SetFibonacciNumber(int numberPosition, int number)
        {
            _cache.Set(numberPosition.ToString(), number, ObjectCache.InfiniteAbsoluteExpiration);
        }
    }
}
