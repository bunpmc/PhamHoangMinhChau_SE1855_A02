using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Repositories;

namespace Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly OrderDetailRepository _orderDetailRepository;

        public OrderDetailService()
        {
            _orderDetailRepository = new OrderDetailRepository();
        }
        public bool AddOrderDetail(OrderDetail newOrderDetail)
        {
            return _orderDetailRepository.AddOrderDetail(newOrderDetail);
        }
        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            return _orderDetailRepository.GetOrderDetailsByOrderId(orderId);
        }
        public bool DeleteOrderDetail(int orderId, int productId)
        {
            return _orderDetailRepository.DeleteOrderDetail(orderId, productId);
        }

        public List<OrderDetail> GetOrderDetails()
        {
            return _orderDetailRepository.GetOrderDetails();
        }

        public bool UpdateOrderDetail(OrderDetail updatedOrderDetail)
        {
            return _orderDetailRepository.UpdateOrderDetail(updatedOrderDetail);
        }
    }
}
