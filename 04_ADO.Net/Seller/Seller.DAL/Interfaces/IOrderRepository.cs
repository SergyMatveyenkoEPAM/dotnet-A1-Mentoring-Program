using Seller.DAL.Models;
using System;

namespace Seller.DAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        void ChangeOrderStatusToInWork(int orderId, DateTime orderDate);

        void ChangeOrderStatusToCompleted(int orderId, DateTime shippedDate);
    }
}
