using NUnit.Framework;
using Seller.DAL.Interfaces;
using Seller.DAL.Models;
using Seller.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Seller.DAL.Tests.UnitTests
{
    [TestFixture]
    public class OrderInformationRepositoryTests
    {
        private string connectionString;
        private IOrderInformationRepository _orderInformationRepository;

        [OneTimeSetUp]
        public void SetVariables()
        {
            connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Northwind; Integrated Security = True";
            _orderInformationRepository = new OrderInformationRepository(connectionString);
        }

        [Test]
        public void GetFullOrderDetails_OrderId_True()
        {
            var _orderRepository = new OrderRepository(connectionString);
            int orderId = _orderRepository.GetAll().FirstOrDefault().OrderID;

            List<FullOrderDetails> fullOrderDetailsList = _orderInformationRepository.GetFullOrderDetails(orderId);

            Assert.True(fullOrderDetailsList.Any());
        }

        [Test]
        public void GetCustomerOrderHistory_CustomerId_True()
        {
            var _orderRepository = new OrderRepository(connectionString);
            string customerId = _orderRepository.GetAll().FirstOrDefault().CustomerID;

            List<OrderHistory> orderHistoryList = _orderInformationRepository.GetCustomerOrderHistory(customerId);

            Assert.True(orderHistoryList.Any());
        }

        [Test]
        public void GetCustomerOrderDetails_OrderId_True()
        {
            var _orderRepository = new OrderRepository(connectionString);
            int orderId = _orderRepository.GetAll().FirstOrDefault().OrderID;

            List<CustomerOrderDetails> customerOrderDetailsList = _orderInformationRepository.GetCustomerOrderDetails(orderId);

            Assert.True(customerOrderDetailsList.Any());
        }
    }
}
