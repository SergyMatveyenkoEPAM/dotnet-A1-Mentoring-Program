using Seller.DAL.Interfaces;
using Seller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Seller.DAL.Repositories
{
    public class OrderInformationRepository : IOrderInformationRepository
    {
        public string ConnectionString { get; set; }

        public OrderInformationRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<FullOrderDetails> GetFullOrderDetails(int orderId)
        {
            var fullOrderDetailsList = new List<FullOrderDetails>();
            string queryString = @"SELECT [CustomerID]
                                          ,o.[EmployeeID]
                                          ,o.[OrderDate]
                                          ,o.[RequiredDate]
                                          ,o.[ShippedDate]
                                          ,o.[ShipVia]
                                          ,o.[Freight]
                                          ,o.[ShipName]
                                          ,o.[ShipAddress]
                                          ,o.[ShipCity]
                                          ,o.[ShipRegion]
                                          ,o.[ShipPostalCode]
                                          ,o.[ShipCountry]
										  ,p.[ProductID]
                                          ,p.[ProductName]
										  ,p.[CategoryID]
										  ,p.[QuantityPerUnit]
										  ,od.[UnitPrice]
										  ,od.[Quantity]
										  ,od.[Discount]
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
                        while (reader.Read())
                        {
                            var fullOrderDetails = new FullOrderDetails
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
                                Status = string.IsNullOrEmpty(reader[2].ToString()) ? Status.New : string.IsNullOrEmpty(reader[4].ToString()) ? Status.InWork : Status.Completed,
                                ProductID = Convert.ToInt32(reader[13]),
                                ProductName = reader[14].ToString(),
                                CategoryID = Convert.ToInt32(reader[15]),
                                QuantityPerUnit = reader[16].ToString(),
                                UnitPrice = Convert.ToDecimal(reader[17]),
                                Quantity = Convert.ToInt32(reader[18]),
                                Discount = Convert.ToDouble(reader[19])
                            };

                            fullOrderDetailsList.Add(fullOrderDetails);
                        }
                    };
                }
            }

            return fullOrderDetailsList;
        }

        public List<OrderHistory> GetCustomerOrderHistory(string customerID)
        {
            var orderHistoryList = new List<OrderHistory>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("CustOrderHist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerID", customerID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var orderHistory = new OrderHistory
                            {
                                ProductName = reader[0].ToString(),
                                TotalQuantity = Convert.ToInt32(reader[1])
                            };

                            orderHistoryList.Add(orderHistory);
                        }
                    }
                }
            }

            return orderHistoryList;
        }

        public List<CustomerOrderDetails> GetCustomerOrderDetails(int orderID)
        {
            var customerOrderDetailsList = new List<CustomerOrderDetails>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("CustOrdersDetail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderID", orderID);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customerOrderDetails = new CustomerOrderDetails
                            {
                                ProductName = reader[0].ToString(),
                                UnitPrice = Convert.ToDecimal(reader[1]),
                                Quantity = Convert.ToInt32(reader[2]),
                                Discount = Convert.ToDouble(reader[3]),
                                ExtendedPrice = Convert.ToDecimal(reader[4])
                            };

                            customerOrderDetailsList.Add(customerOrderDetails);
                        }
                    }
                }
            }

            return customerOrderDetailsList;
        }
    }
}
