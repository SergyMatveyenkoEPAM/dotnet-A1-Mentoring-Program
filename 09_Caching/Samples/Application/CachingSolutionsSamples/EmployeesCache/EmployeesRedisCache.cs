using NorthwindLibrary;
using StackExchange.Redis;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace CachingSolutionsSamples.EmployeesCache
{
    class EmployeesRedisCache : IEmployeesCache
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private const string Prefix = "Cache_Employees";

        readonly DataContractSerializer _serializer = new DataContractSerializer(
            typeof(IEnumerable<Employee>));

        public EmployeesRedisCache(string hostName)
        {
            _redisConnection = ConnectionMultiplexer.Connect(hostName);
        }

        public IEnumerable<Employee> Get(string forUser)
        {
            var db = _redisConnection.GetDatabase();
            byte[] s = db.StringGet(Prefix + forUser);
            if (s == null)
                return null;

            return (IEnumerable<Employee>)_serializer.ReadObject(new MemoryStream(s));
        }

        public void Set(string forUser, IEnumerable<Employee> employees)
        {
            var db = _redisConnection.GetDatabase();
            var key = Prefix + forUser;

            if (employees == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                _serializer.WriteObject(stream, employees);
                db.StringSet(key, stream.ToArray());
            }
        }
    }
}
