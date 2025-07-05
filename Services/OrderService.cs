using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Repositories;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;
        public OrderService()
        {
            _orderRepository = new OrderRepository();
        }
        public bool AddOrder(Order order)
        {
            return _orderRepository.AddOrder(order);
        }

        public bool DeleteOrder(Order order)
        {
            return _orderRepository.DeleteOrder(order);
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }

        public bool UpdateOrder(Order order)
        {
            return _orderRepository.UpdateOrder(order);
        }
    }
}
