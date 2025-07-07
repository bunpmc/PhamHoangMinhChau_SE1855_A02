using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        OrderDetailDAO od = new OrderDetailDAO();
        public bool AddOrderDetail(OrderDetail orderDetail)
        {
            return od.AddOrderDetail(orderDetail);
        }
        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            return od.GetOrderDetailsByOrderId(orderId);
        }

        public bool DeleteOrderDetail(int orderId, int productId)
        {
            return od.DeleteOrderDetail(orderId, productId);
        }

        public List<OrderDetail> GetOrderDetails()
        {
            return od.GetOrderDetails();
        }

        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            return od.UpdateOrderDetail(orderDetail);
        }
    }
}
