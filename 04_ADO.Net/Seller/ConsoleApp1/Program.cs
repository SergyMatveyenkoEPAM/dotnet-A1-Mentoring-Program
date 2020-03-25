using System;
using System.Configuration;
using System.Linq;
using Seller.DAL.Repositories;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString;


            var _categoryRepository = new CategoryRepository(connectionString);

            var category = _categoryRepository.GetById(1);




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

            foreach (var item in res.ProductNames)
            {
                Console.WriteLine(item);
            }

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
