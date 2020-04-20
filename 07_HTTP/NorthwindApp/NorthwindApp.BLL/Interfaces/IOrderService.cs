using NorthwindApp.BLL.Models;
using System.Collections.Generic;

namespace NorthwindApp.BLL.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetAll();
    }
}
