using NorthwindApp.DAL.Models;
using System.Linq;

namespace NorthwindApp.DAL.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll();
    }
}
