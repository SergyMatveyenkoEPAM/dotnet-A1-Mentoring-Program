using NorthwindLibrary;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace CachingSolutionsSamples.SuppliersCache
{
    internal class SuppliersMemoryCache : ISuppliersCache
    {
        readonly ObjectCache _cache = MemoryCache.Default;
        private const string Prefix = "Cache_Suppliers";

        public IEnumerable<Supplier> Get(string forUser)
        {
            return (IEnumerable<Supplier>)_cache.Get(Prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<Supplier> employees)
        {
            _cache.Set(Prefix + forUser, employees, ObjectCache.InfiniteAbsoluteExpiration);
        }
    }
}
