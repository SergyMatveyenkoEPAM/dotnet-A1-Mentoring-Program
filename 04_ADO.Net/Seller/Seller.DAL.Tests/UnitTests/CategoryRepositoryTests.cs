using NUnit.Framework;
using Seller.DAL.Models;
using Seller.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Seller.DAL.Interfaces;

namespace Seller.DAL.Tests.UnitTests
{
    [TestFixture]
    public class CategoryRepositoryTests
    {
        private string connectionString;
        private Category category;
        private IRepository<Category> _categoryRepository;

        [OneTimeSetUp]
        public void SetVariables()
        {
            // connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString;
            connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Northwind; Integrated Security = True";
            _categoryRepository = new CategoryRepository(connectionString);
            category = new Category
            {
                CategoryName = "Wild berries",
                Description = "Natural product",
                Picture = new byte[10]
            };
        }

        [Test]
        public void Add_Category_CategoryCount()
        {
            int count = _categoryRepository.GetAll().Count();

            _categoryRepository.Add(category);

            Assert.AreEqual(count + 1, _categoryRepository.GetAll().Count());
        }

        [Test]
        public void Delete_LastCategoryId_CategoryCount()
        {
            int categoryId = _categoryRepository.GetAll().LastOrDefault().CategoryID;
            int count = _categoryRepository.GetAll().Count();

            _categoryRepository.Delete(categoryId);

            Assert.AreEqual(count - 1, _categoryRepository.GetAll().Count());
        }

        [Test]
        public void GetAll_Categories_CategoryCount()
        {
            _categoryRepository.Add(category);
            _categoryRepository.Add(category);

            IEnumerable<Category> categoryList = _categoryRepository.GetAll();

            Assert.True(categoryList.Count() > 1);
        }

        [Test]
        public void GetById_LastCategoryId_True()
        {
            _categoryRepository.Add(category);
            int categoryId = _categoryRepository.GetAll().LastOrDefault().CategoryID;

            Category categoryFromRepository = _categoryRepository.GetById(categoryId);

            Assert.True(categoryFromRepository.CategoryName == category.CategoryName && categoryFromRepository.Description == category.Description && categoryFromRepository.Picture.SequenceEqual(category.Picture));
        }

        [Test]
        public void Update_Category_True()
        {
            string categoryName = "Can";
            string description = "Sea Can";
            byte[] picture = new byte[25];
            Category categoryFromRepository = _categoryRepository.GetAll().LastOrDefault();
            categoryFromRepository.CategoryName = categoryName;
            categoryFromRepository.Description = description;
            categoryFromRepository.Picture = picture;

            _categoryRepository.Update(categoryFromRepository);
            Category updatedCategoryFromRepository = _categoryRepository.GetById(categoryFromRepository.CategoryID);

            Assert.True(updatedCategoryFromRepository.CategoryName == categoryName && updatedCategoryFromRepository.Description == description && updatedCategoryFromRepository.Picture.SequenceEqual(picture));
        }
    }
}
