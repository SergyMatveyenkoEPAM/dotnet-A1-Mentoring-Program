using NorthwindLibrary;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace CachingSolutionsSamples.EmployeesCache
{
    internal class EmployeesMemoryCache : IEmployeesCache
    {
        readonly ObjectCache _cache = MemoryCache.Default;
        private const string Prefix = "Cache_Employees";

        public IEnumerable<Employee> Get(string forUser)
        {
            return (IEnumerable<Employee>)_cache.Get(Prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<Employee> employees)
        {
            _cache.Set(Prefix + forUser, employees, ObjectCache.InfiniteAbsoluteExpiration);
        }
    }
}
