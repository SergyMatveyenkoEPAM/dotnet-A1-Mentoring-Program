using CachingSolutionsSamples.EmployeesCache;
using CachingSolutionsSamples.SuppliersCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples
{
    [TestClass]
    public class CacheTests
    {
        [TestMethod]
        public void MemoryCache()
        {
            var categoryManager = new CategoriesManager(new CategoriesMemoryCache());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(categoryManager.GetCategories().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void RedisCache()
        {
            var categoryManager = new CategoriesManager(new CategoriesRedisCache("localhost"));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(categoryManager.GetCategories().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void MemoryCache_Employees()
        {
            var employeesManager = new EmployeesManager(new EmployeesMemoryCache());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(employeesManager.GetEmployees().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void MemoryCache_Suppliers()
        {
            var suppliersManager = new SuppliersManager(new SuppliersMemoryCache());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(suppliersManager.GetSuppliers().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void RedisCache_Employees()
        {
            var employeesManager = new EmployeesManager(new EmployeesRedisCache("localhost"));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(employeesManager.GetEmployees().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void RedisCache_Suppliers()
        {
            var suppliersManager = new SuppliersManager(new SuppliersRedisCache("localhost"));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(suppliersManager.GetSuppliers().Count());
                Thread.Sleep(100);
            }
        }



    }
}
