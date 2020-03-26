using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using Seller.DAL.Models;
using Seller.DAL.Repositories;

namespace Seller.DAL.Tests1
{
    /*[TestFixture]
    public class Class1
    {

        [Test]
        public void Add_Category_CategoryCount()
        {
            string connectionString1 = ConfigurationManager.AppSettings["SQLConnString"];
            string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString;
            CategoryRepository _categoryRepository = new CategoryRepository(connectionString);

            var category = new Category
            {
                CategoryName = "Wild berries",
                Description = "Natural product",
                Picture = new byte[10]
            };
            int count = _categoryRepository.GetAll().Count();

            _categoryRepository.Add(category);

            Assert.AreEqual(count + 1, _categoryRepository.GetAll().Count());
        }

    }*/
}
