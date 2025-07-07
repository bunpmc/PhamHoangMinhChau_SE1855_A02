using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OrderDetailDAO
    {
        LucySalesDataContext context = new LucySalesDataContext();
        public List<OrderDetail> GetOrderDetails()
        {
            return context.OrderDetails.ToList();
        }
        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            return context.OrderDetails.Where(od => od.OrderId == orderId).ToList();
        }
        public bool AddOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                context.OrderDetails.Add(orderDetail);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding order detail: {ex.Message}");
                return false;
            }
        }
        public bool DeleteOrderDetail(int orderId, int productId)
        {
            try
            {
                OrderDetail? orderDetail = context.OrderDetails.FirstOrDefault(p => p.OrderId == orderId && p.ProductId == productId);
                if (orderDetail == null)
                {
                    Console.WriteLine("Order detail not found.");
                    return false;
                }

                context.OrderDetails.Remove(orderDetail);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting order detail: {ex.Message}");
                return false;
            }
        }
        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            try 
            {
                var existingOrderDetail = context.OrderDetails.Find(orderDetail.OrderId, orderDetail.ProductId);
                if (existingOrderDetail != null)
                {
                    existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
                    existingOrderDetail.Quantity = orderDetail.Quantity;
                    existingOrderDetail.Discount = orderDetail.Discount;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order detail: {ex.Message}");
                return false;
            }
        }
    }
}
