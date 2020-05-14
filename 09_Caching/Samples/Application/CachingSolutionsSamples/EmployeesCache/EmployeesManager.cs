using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples.EmployeesCache
{
    public class EmployeesManager
    {
        private readonly IEmployeesCache _cache;

        public EmployeesManager(IEmployeesCache cache)
        {
            this._cache = cache;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            Console.WriteLine("Get Employees");

            var user = Thread.CurrentPrincipal.Identity.Name;
            var employees = _cache.Get(user);

            if (employees == null)
            {
                Console.WriteLine("From DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    employees = dbContext.Employees.ToList();
                    _cache.Set(user, employees);
                }
            }

            return employees;
        }
    }
}
