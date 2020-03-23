using System;
using System.Data.SqlClient;
using System.Configuration;
using Seller.DAL.Interfaces;
using Seller.DAL.Models;
using System.Collections.Generic;

namespace Seller.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        public string ConnectionString { get; set; }
        public OrderRepository(string connectionString = null)
        {
            ConnectionString = connectionString ?? @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";// ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString;
        }

        public void Add(Order item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int orderId)
        {
            throw new NotImplementedException();
        }

        public Order Get(int orderId)
        {
            Order order = null;
            string queryString = @"SELECT [CustomerID]
                                          ,[EmployeeID]
                                          ,[OrderDate]
                                          ,[RequiredDate]
                                          ,[ShippedDate]
                                          ,[ShipVia]
                                          ,[Freight]
                                          ,[ShipName]
                                          ,[ShipAddress]
                                          ,[ShipCity]
                                          ,[ShipRegion]
                                          ,[ShipPostalCode]
                                          ,[ShipCountry]
                                          ,[ProductName]
                                     FROM [dbo].[Orders] o INNER JOIN [dbo].[Order Details] od ON o.[OrderID]=od.[OrderID]
                                                           INNER JOIN [dbo].[Products] p ON od.[ProductID]=p.[ProductID]
                                     WHERE o.[OrderID] = @OrderID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryString, connection))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            order = new Order
                            {
                                OrderID = orderId,
                                CustomerID = reader[0].ToString(),
                                EmployeeID = Convert.ToInt32(reader[1]),
                                OrderDate = string.IsNullOrEmpty(reader[2].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader[2]),
                                RequiredDate = string.IsNullOrEmpty(reader[3].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader[3]),
                                ShippedDate = string.IsNullOrEmpty(reader[4].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader[4]),
                                ShipVia = Convert.ToInt32(reader[5]),
                                Freight = Convert.ToDecimal(reader[6]),
                                ShipName = reader[7].ToString(),
                                ShipAddress = reader[8].ToString(),
                                ShipCity = reader[9].ToString(),
                                ShipRegion = reader[10].ToString(),
                                ShipPostalCode = reader[11].ToString(),
                                ShipCountry = reader[12].ToString(),
                                Status = string.IsNullOrEmpty(reader[2].ToString()) ? Status.New : string.IsNullOrEmpty(reader[4].ToString()) ? Status.InWork : Status.Completed
                            };

                            do
                            {
                                order.ProductNames.Add(reader[13].ToString());
                            } while (reader.Read());

                        }
                    };
                }
            }

            return order;
        }

        public void Update(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
