using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Seller.DAL.Models;
using Seller.DAL.Repositories;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString;


            var _categoryRepository = new CategoryRepository(connectionString);
            var category1  = _categoryRepository.GetById(1);
            
            var category = new Category
            {
                CategoryName = "Wild berries",
                Description = "Natural product",
                Picture = new byte[10]
            };
            int count = _categoryRepository.GetAll().Count();

            _categoryRepository.Add(category);





            var _orderStatisticRepository = new OrderInformationRepository(connectionString);
            List<OrderHistory> orderHistoryList = _orderStatisticRepository.GetCustomerOrderHistory("VINET");
            foreach (var orderHistory in orderHistoryList)
            {
                // Console.WriteLine(orderHistory.ProductName + " " + orderHistory.TotalQuantity);
            }
            List<CustomerOrderDetails> customerOrderDetailsList = _orderStatisticRepository.GetCustomerOrderDetails(10248);
            foreach (var customerOrderDetails in customerOrderDetailsList)
            {
                // Console.WriteLine(customerOrderDetails.ProductName + " " + customerOrderDetails.UnitPrice + " " + customerOrderDetails.Quantity + " " + customerOrderDetails.Discount + " " + customerOrderDetails.ExtendedPrice);
            }

            List<FullOrderDetails> fullOrderDetailsList = _orderStatisticRepository.GetFullOrderDetails(10248);
            foreach (var fullOrderDetails in fullOrderDetailsList)
            {
                foreach (var property in fullOrderDetails.GetType().GetProperties())
                {
                    Console.Write(property.GetValue(fullOrderDetails, null) + " ");
                }

                Console.WriteLine("------------");
            }





            Console.WriteLine("Hello World!");
            var picture = category.Picture.Skip(78);
            var rubbish = category.Picture.Take(78);

            var orderRepository = new OrderRepository(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString);
            foreach (var item in rubbish)
            {
                Console.Write(item);
            }

            Console.WriteLine("\n---------------------------------");

            var res = orderRepository.GetById(11077);
            Console.WriteLine(res.OrderID.ToString());
            Console.WriteLine(res.CustomerID.ToString());
            Console.WriteLine(res.EmployeeID.ToString());
            Console.WriteLine(res.OrderDate.ToString());
            Console.WriteLine(res.RequiredDate.ToString());
            Console.WriteLine(res.ShippedDate.ToString());
            Console.WriteLine(res.ShipVia.ToString());
            Console.WriteLine(res.Freight.ToString());
            Console.WriteLine(res.ShipName.ToString());
            Console.WriteLine(res.ShipAddress.ToString());
            Console.WriteLine(res.ShipCity.ToString());
            Console.WriteLine(res.ShipRegion.ToString());
            Console.WriteLine(res.ShipPostalCode.ToString());
            Console.WriteLine(res.ShipCountry.ToString());
            Console.WriteLine(res.Status.ToString());

            Console.WriteLine("---------------------------------ProductNames---------------------------------");



            //  orderRepository.Add(res);
            /*res.OrderID = 11081;
            res.ShipCity = "Gomel";
            
            orderRepository.Update(res);*/


            //foreach (var order in orderRepository.GetAll())
            //{
            //    Console.Write("\n" + order.OrderID + " ");
            //    foreach (var productName in order.ProductNames)
            //    {
            //        Console.Write(productName);
            //    }
            //}

            //  orderRepository.Delete(11080);
            Console.ReadKey();
        }
    }
}
