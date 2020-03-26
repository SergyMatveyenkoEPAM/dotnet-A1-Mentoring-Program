using System;
using System.Data.SqlClient;
using System.Configuration;
using Seller.DAL.Interfaces;
using Seller.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Seller.DAL.Exceptions;

namespace Seller.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public string ConnectionString { get; set; }
        public OrderRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Add(Order order)
        {
            string queryString = @"INSERT INTO [dbo].[Orders] ([CustomerID]
                                                              ,[EmployeeID]
                                                              ,[RequiredDate]
                                                              ,[ShipVia]
                                                              ,[Freight]
                                                              ,[ShipName]
                                                              ,[ShipAddress]
                                                              ,[ShipCity]
                                                              ,[ShipRegion]
                                                              ,[ShipPostalCode]
                                                              ,[ShipCountry])
                                   VALUES (@CustomerID, @EmployeeID, @RequiredDate, @ShipVia, @Freight, @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                    command.Parameters.AddWithValue("@EmployeeID", order.EmployeeID);
                    command.Parameters.AddWithValue("@RequiredDate", order.RequiredDate);
                    command.Parameters.AddWithValue("@ShipVia", order.ShipVia);
                    command.Parameters.AddWithValue("@Freight", order.Freight);
                    command.Parameters.AddWithValue("@ShipName", order.ShipName);
                    command.Parameters.AddWithValue("@ShipAddress", order.ShipAddress);
                    command.Parameters.AddWithValue("@ShipCity", order.ShipCity);
                    command.Parameters.AddWithValue("@ShipRegion", order.ShipRegion);
                    command.Parameters.AddWithValue("@ShipPostalCode", order.ShipPostalCode);
                    command.Parameters.AddWithValue("@ShipCountry", order.ShipCountry);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int orderId)
        {
            var order = GetById(orderId);
            if (order.Status == Status.New)
            {
                throw new ProhibitedOperationException("Deleting orders with the status of \"New\" is prohibited");
            }

            if (order.Status == Status.InWork)
            {
                throw new ProhibitedOperationException("Deleting orders with the status of \"In work\" is prohibited");
            }

            string queryString = @"DELETE [dbo].[Orders] 
                                     WHERE [OrderID] = @OrderID";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@OrderID", orderId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public Order GetById(int orderId)
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
                                     FROM [dbo].[Orders]
                                     WHERE [OrderID] = @OrderID";

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
                        }
                    };
                }
            }

            return order;
        }

        public void Update(Order order)
        {
            var existingOrder = GetById(order.OrderID);

            if (existingOrder.Status == Status.InWork)
            {
                throw new ProhibitedOperationException("Changing orders with the status of \"In work\" is prohibited");
            }
            if (existingOrder.Status == Status.Completed)
            {
                throw new ProhibitedOperationException("Changing orders with the status of \"Completed\" is prohibited");
            }
            if (existingOrder.Status != order.Status)
            {
                throw new ProhibitedOperationException("Changing order's status is prohibited");
            }
            if (existingOrder.OrderDate != order.OrderDate)
            {
                throw new ProhibitedOperationException("Changing order's date is prohibited");
            }
            if (existingOrder.ShippedDate != order.ShippedDate)
            {
                throw new ProhibitedOperationException("Changing order's shipped date is prohibited");
            }

            string queryString = @"UPDATE [dbo].[Orders]
                                       SET [CustomerID] = @CustomerID
                                          ,[EmployeeID] = @EmployeeID
                                          ,[RequiredDate] = @RequiredDate
                                          ,[ShipVia] = @ShipVia
                                          ,[Freight] = @Freight
                                          ,[ShipName] = @ShipName
                                          ,[ShipAddress] = @ShipAddress
                                          ,[ShipCity] = @ShipCity
                                          ,[ShipRegion] = @ShipRegion
                                          ,[ShipPostalCode] = @ShipPostalCode
                                          ,[ShipCountry] = @ShipCountry
                                     WHERE [OrderID] = @OrderID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@OrderID", order.OrderID);
                    command.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                    command.Parameters.AddWithValue("@EmployeeID", order.EmployeeID);
                    command.Parameters.AddWithValue("@RequiredDate", order.RequiredDate);
                    command.Parameters.AddWithValue("@ShipVia", order.ShipVia);
                    command.Parameters.AddWithValue("@Freight", order.Freight);
                    command.Parameters.AddWithValue("@ShipName", order.ShipName);
                    command.Parameters.AddWithValue("@ShipAddress", order.ShipAddress);
                    command.Parameters.AddWithValue("@ShipCity", order.ShipCity);
                    command.Parameters.AddWithValue("@ShipRegion", order.ShipRegion);
                    command.Parameters.AddWithValue("@ShipPostalCode", order.ShipPostalCode);
                    command.Parameters.AddWithValue("@ShipCountry", order.ShipCountry);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Order> GetAll()
        {
            var orderList = new List<Order>();
            string queryString = @"SELECT o.[OrderID]
                                          ,[CustomerID]
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
                                     ORDER BY o.[OrderID]";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new Order
                            {
                                OrderID = Convert.ToInt32(reader[0]),
                                CustomerID = reader[1].ToString(),
                                EmployeeID = Convert.ToInt32(reader[2]),
                                OrderDate = string.IsNullOrEmpty(reader[3].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader[3]),
                                RequiredDate = string.IsNullOrEmpty(reader[4].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader[4]),
                                ShippedDate = string.IsNullOrEmpty(reader[5].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader[5]),
                                ShipVia = Convert.ToInt32(reader[6]),
                                Freight = Convert.ToDecimal(reader[7]),
                                ShipName = reader[8].ToString(),
                                ShipAddress = reader[9].ToString(),
                                ShipCity = reader[10].ToString(),
                                ShipRegion = reader[11].ToString(),
                                ShipPostalCode = reader[12].ToString(),
                                ShipCountry = reader[13].ToString(),
                                Status = string.IsNullOrEmpty(reader[3].ToString()) ? Status.New : string.IsNullOrEmpty(reader[5].ToString()) ? Status.InWork : Status.Completed
                            };

                            orderList.Add(order);
                        }
                    };
                }
            }

            return orderList;
        }

        public void ChangeOrderStatusToInWork(int orderId, DateTime orderDate)
        {
            string queryString = @"UPDATE [dbo].[Orders]
                                       SET [OrderDate] = @OrderDate
                                     WHERE [OrderID] = @OrderID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@OrderID", orderId);
                    command.Parameters.AddWithValue("@OrderDate", orderDate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ChangeOrderStatusToCompleted(int orderId, DateTime shippedDate)
        {
            string queryString = @"UPDATE [dbo].[Orders]
                                       SET [ShippedDate] = @ShippedDate
                                     WHERE [OrderID] = @OrderID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@OrderID", orderId);
                    command.Parameters.AddWithValue("@ShippedDate", shippedDate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
