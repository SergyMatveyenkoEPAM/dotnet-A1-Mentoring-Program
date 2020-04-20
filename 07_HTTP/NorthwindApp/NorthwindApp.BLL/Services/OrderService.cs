using System.Collections.Generic;
using AutoMapper;
using NorthwindApp.BLL.Interfaces;
using NorthwindApp.BLL.Models;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.DAL.Models;
using NorthwindApp.DAL.Repositories;
using System.Linq;

namespace NorthwindApp.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService()
        {
            _repository = new OrderRepository();
        }

        public IEnumerable<OrderDTO> GetAll()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDTO>().ReverseMap();
            });


            IMapper mapper = config.CreateMapper();
            var dest = mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(_repository.GetAll());
            

            return dest;
        }
    }
}
