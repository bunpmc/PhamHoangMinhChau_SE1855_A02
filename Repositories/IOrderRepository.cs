using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Repositories
{
    public interface IOrderRepository
    {
        public List<Order> GetOrders();
        public bool AddOrder(Order order);
        public bool DeleteOrder(Order order);
        public bool UpdateOrder(Order order);

    }
}
