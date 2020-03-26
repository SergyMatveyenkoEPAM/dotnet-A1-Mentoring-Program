using Seller.DAL.Models;
using System.Collections.Generic;

namespace Seller.DAL.Interfaces
{
    interface IOrderInformationRepository
    {
        string ConnectionString { get; set; }

        List<FullOrderDetails> GetFullOrderDetails(int orderId);

        List<OrderHistory> GetCustomerOrderHistory(string customerID);

        List<CustomerOrderDetails> GetCustomerOrderDetails(int orderID);
    }
}
