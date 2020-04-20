using NorthwindApp.DAL.Interfaces;
using NorthwindApp.DAL.Models;
using System.Linq;

namespace NorthwindApp.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NorthwindDB _db;

        public OrderRepository()
        {
            _db = new NorthwindDB();
        }

        public IQueryable<Order> GetAll()
        {
            return _db.Orders;
        }
    }
}
