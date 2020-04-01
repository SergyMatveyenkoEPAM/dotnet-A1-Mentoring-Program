using EFNorthwind.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new NorthwindContext();
            foreach (var order in context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.Category).Where(o => o.OrderDetails.Any(od => od.Product.Category.CategoryName == "Condiments")))
            {
                Console.WriteLine(order.OrderId + " " + order.OrderDate + " " + order.ShipCity + " " + order.ShipCountry + " " + order.OrderDetails?.FirstOrDefault()?.Product.ProductName + " " + order.OrderDetails?.FirstOrDefault()?.Product.Category.CategoryName);
                Console.WriteLine("OrderDetails");
                foreach (var orderDetails in order.OrderDetails)
                {
                    Console.WriteLine(orderDetails.Product.Category.CategoryName + " " + orderDetails.Product.ProductName + " " + orderDetails.Quantity + " " + orderDetails.UnitPrice);
                }

                Console.WriteLine("-----------------------------------------------");
            }

            Console.ReadKey();
        }
    }
}
