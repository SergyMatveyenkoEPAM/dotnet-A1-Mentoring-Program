using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples.SuppliersCache
{
    public class SuppliersManager
    {
        private ISuppliersCache cache;

        public SuppliersManager(ISuppliersCache cache)
        {
            this.cache = cache;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            Console.WriteLine("Get Suppliers");

            var user = Thread.CurrentPrincipal.Identity.Name;
            var suppliers = cache.Get(user);

            if (suppliers == null)
            {
                Console.WriteLine("From DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    suppliers = dbContext.Suppliers.ToList();
                    cache.Set(user, suppliers);
                }
            }

            return suppliers;
        }
    }
}
