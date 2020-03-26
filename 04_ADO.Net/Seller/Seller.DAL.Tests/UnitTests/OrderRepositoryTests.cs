using NUnit.Framework;
using Seller.DAL.Exceptions;
using Seller.DAL.Models;
using Seller.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Seller.DAL.Tests.UnitTests
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        private string connectionString;
        private Order order;

        [OneTimeSetUp]
        public void SetVariables()
        {
            // connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString;
            connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Northwind; Integrated Security = True";
            order = new Order
            {
                CustomerID = "VINET",
                EmployeeID = 5,
                RequiredDate = Convert.ToDateTime("1996-08-01 00:00:00.000"),
                ShipVia = 3,
                Freight = 32.38m,
                ShipName = "Vins et alcools Chevalier",
                ShipAddress = "59 rue de l'Abbaye",
                ShipCity = "Gomel",
                ShipRegion = null,
                ShipPostalCode = "51100",
                ShipCountry = "Belarus",
            };
        }

        [Test]
        public void Add_Order_OrderCount()
        {
            OrderRepository _orderRepository = new OrderRepository(connectionString);
            int count = _orderRepository.GetAll().Count();

            _orderRepository.Add(order);

            Assert.AreEqual(count + 1, _orderRepository.GetAll().Count());
        }

        [Test]
        public void Delete_Order_OrderCount()
        {
            OrderRepository _orderRepository = new OrderRepository(connectionString);
            _orderRepository.Add(order);
            int count = _orderRepository.GetAll().Count();
            int orderId = _orderRepository.GetAll().LastOrDefault().OrderID;
            _orderRepository.ChangeOrderStatusToInWork(orderId, DateTime.Now);
            _orderRepository.ChangeOrderStatusToCompleted(orderId, DateTime.Now);

            _orderRepository.Delete(orderId);

            Assert.AreEqual(count - 1, _orderRepository.GetAll().Count());
        }

        [Test]
        public void Delete_NewOrder_GetProhibitedOperationException()
        {
            OrderRepository _orderRepository = new OrderRepository(connectionString);
            _orderRepository.Add(order);
            int orderId = _orderRepository.GetAll().LastOrDefault().OrderID;

            Assert.Throws<ProhibitedOperationException>(() => _orderRepository.Delete(orderId));
        }

        [Test]
        public void GetById_OrderId_True()
        {
            OrderRepository _orderRepository = new OrderRepository(connectionString);
            _orderRepository.Add(order);
            int orderId = _orderRepository.GetAll().LastOrDefault().OrderID;

            Order orderFromRepository = _orderRepository.GetById(orderId);

            Assert.True(order.CustomerID == orderFromRepository.CustomerID && order.RequiredDate == orderFromRepository.RequiredDate && order.ShipCountry == orderFromRepository.ShipCountry);
        }

        [Test]
        public void GetAll_Orders_OrderCount()
        {
            OrderRepository _orderRepository = new OrderRepository(connectionString);
            _orderRepository.Add(order);
            _orderRepository.Add(order);

            IEnumerable<Order> orderList = _orderRepository.GetAll();

            Assert.True(orderList.Count() > 1);
        }

        [Test]
        public void Update_Order_True()
        {
            string shipCity = "Moskow";
            string shipCountry = "Russia";
            int employeeID = 1;
            OrderRepository _orderRepository = new OrderRepository(connectionString);
            Order orderFromRepository = _orderRepository.GetAll().LastOrDefault();
            orderFromRepository.ShipCity = shipCity;
            orderFromRepository.ShipCountry = shipCountry;
            orderFromRepository.EmployeeID = employeeID;

            _orderRepository.Update(orderFromRepository);
            Order updatedOrderFromRepository = _orderRepository.GetById(orderFromRepository.OrderID);

            Assert.True(updatedOrderFromRepository.ShipCity == shipCity && updatedOrderFromRepository.ShipCountry == shipCountry && updatedOrderFromRepository.EmployeeID == employeeID);
        }

        [Test]
        public void ChangeOrderStatusToInWork_OrderId_True()
        {
            var time = DateTime.Now.Date;
            OrderRepository _orderRepository = new OrderRepository(connectionString);

            int orderId = _orderRepository.GetAll().LastOrDefault().OrderID;
            _orderRepository.ChangeOrderStatusToInWork(orderId, time);

            Order orderFromRepository = _orderRepository.GetById(orderId);

            Assert.AreEqual(orderFromRepository.OrderDate, time);
        }


        [Test]
        public void ChangeOrderStatusToCompleted_OrderId_True()
        {
            var time = DateTime.Now.Date;
            OrderRepository _orderRepository = new OrderRepository(connectionString);

            int orderId = _orderRepository.GetAll().LastOrDefault().OrderID;
            _orderRepository.ChangeOrderStatusToCompleted(orderId, time);

            Order orderFromRepository = _orderRepository.GetById(orderId);

            Assert.AreEqual(orderFromRepository.ShippedDate, time);
        }
    }
}
