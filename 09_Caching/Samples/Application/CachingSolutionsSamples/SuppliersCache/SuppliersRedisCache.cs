using NorthwindLibrary;
using StackExchange.Redis;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace CachingSolutionsSamples.SuppliersCache
{
    class SuppliersRedisCache : ISuppliersCache
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private const string Prefix = "Cache_Suppliers";

        readonly DataContractSerializer _serializer = new DataContractSerializer(
            typeof(IEnumerable<Category>));

        public SuppliersRedisCache(string hostName)
        {
            _redisConnection = ConnectionMultiplexer.Connect(hostName);
        }
        public IEnumerable<Supplier> Get(string forUser)
        {
            var db = _redisConnection.GetDatabase();
            byte[] s = db.StringGet(Prefix + forUser);
            if (s == null)
                return null;

            return (IEnumerable<Supplier>)_serializer.ReadObject(new MemoryStream(s));
        }

        public void Set(string forUser, IEnumerable<Supplier> suppliers)
        {
            var db = _redisConnection.GetDatabase();
            var key = Prefix + forUser;

            if (suppliers == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                _serializer.WriteObject(stream, suppliers);
                db.StringSet(key, stream.ToArray());
            }
        }
    }
}
