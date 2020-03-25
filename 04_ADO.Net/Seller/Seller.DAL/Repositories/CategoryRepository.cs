using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Seller.DAL.Interfaces;
using Seller.DAL.Models;

namespace Seller.DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private const int RubbishSize = 78;
        public string ConnectionString { get; set; }

        public CategoryRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Add(Category category)
        {
            string queryString = @"INSERT INTO [dbo].[Categories] ([CategoryName]
                                                                  ,[Description]
                                                                  ,[Picture])
                                   VALUES (@CategoryName, @Description, @Picture)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    command.Parameters.AddWithValue("@Description", category.Description);
                    command.Parameters.AddWithValue("@Picture", GetRubbish(RubbishSize).Concat(category.Picture));

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private byte[] GetRubbish(int rubbishSize)
        {
            var random = new Random();
            var rubbish = new byte[rubbishSize];
            random.NextBytes(rubbish);
            return rubbish;
        }

        public void Delete(int categoryId)
        {
            string queryString = @"DELETE [dbo].[Categories] 
                                     WHERE [CategoryID] = @CategoryID";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", categoryId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Category> GetAll()
        {
            var categoryList = new List<Category>();
            string queryString = @"SELECT [CategoryID]
                                         ,[CategoryName]
                                         ,[Description]
                                         ,[Picture]                                         
                                     FROM [dbo].[Categories]";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var category = new Category
                            {
                                CategoryID = Convert.ToInt32(reader[0]),
                                CategoryName = reader[1].ToString(),
                                Description = reader[2].ToString(),
                                Picture = ((byte[])reader[3]).Skip(RubbishSize).ToArray()
                            };

                            categoryList.Add(category);
                        }
                    };
                }
            }

            return categoryList;
        }

        public Category GetById(int categoryId)
        {
            Category category = null;
            string queryString = @"SELECT [CategoryName]
                                         ,[Description]
                                         ,[Picture]                                         
                                     FROM [dbo].[Categories]                                         
                                     WHERE [CategoryID] = @CategoryID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryString, connection))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            category = new Category
                            {
                                CategoryID = categoryId,
                                CategoryName = reader[0].ToString(),
                                Description = reader[1].ToString(),
                                Picture = ((byte[])reader[2]).Skip(RubbishSize).ToArray()
                            };
                        }
                    };
                }
            }

            return category;
        }

        public void Update(Category category)
        {
            string queryString = @"UPDATE [dbo].[Categories]
                                       SET [CategoryName] = @CategoryName
                                          ,[Description] = @Description
                                          ,[Picture] = @Picture                                          
                                     WHERE [CategoryID] = @CategoryID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                    command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    command.Parameters.AddWithValue("@Description", category.Description);
                    command.Parameters.AddWithValue("@Picture", GetRubbish(RubbishSize).Concat(category.Picture));

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
