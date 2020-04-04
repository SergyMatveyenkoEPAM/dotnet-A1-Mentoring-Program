using EFNorthwind.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace EFNorthwind.Tests
{
    public class NorthwindTests
    {
        [Test]
        public void GetOrders_Context_SeeTheLog()
        {
            using var context = new NorthwindContext();
            context.Database.EnsureCreated();

            foreach (var order in context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.Category).Include(o => o.Customer).Where(o => o.OrderDetails.Any(od => od.Product.Category.CategoryName == "Condiments")))
            {
                Console.WriteLine(order.OrderId + " " + order.OrderDate?.ToShortDateString() + " " + order.ShipCountry);
                Console.WriteLine("Company name: " + order.Customer.CompanyName);
                Console.WriteLine("Order details:");
                foreach (var orderDetails in order.OrderDetails)
                {
                    Console.WriteLine(orderDetails.Product.Category.CategoryName + " " + orderDetails.Product.ProductName + " " + orderDetails.UnitPrice);
                }

                Console.WriteLine("-----------------------------------------------");
            }
        }
    }
}