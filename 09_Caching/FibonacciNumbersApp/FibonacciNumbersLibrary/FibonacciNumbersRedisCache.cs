using StackExchange.Redis;

namespace FibonacciNumbersLibrary
{
    public class FibonacciNumbersRedisCache : IFibonacciCache
    {
        private readonly IDatabase _db;

        public FibonacciNumbersRedisCache()
        {
            var redisConnection = ConnectionMultiplexer.Connect("localhost:6947");
            _db = redisConnection.GetDatabase();
        }

        public int? GetFibonacciNumber(int numberPosition)
        {
            return (int?)_db.StringGet(numberPosition.ToString());
        }

        public void SetFibonacciNumber(int numberPosition, int number)
        {
            _db.StringSet(numberPosition.ToString(), number);
        }
    }
}
