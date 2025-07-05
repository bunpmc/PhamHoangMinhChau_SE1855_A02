using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        OrderDAO od = new OrderDAO();

        public bool AddOrder(Order order)
        {
            return od.AddOrder(order);
        }

        public bool DeleteOrder(Order order)
        {
            return od.DeleteOrder(order);
        }

        public List<Order> GetOrders()
        {
            return od.GetOrders();
        }

        public bool UpdateOrder(Order order)
        {
            return od.UpdateOrder(order);
        }
    }
}
