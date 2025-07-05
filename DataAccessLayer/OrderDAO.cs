using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OrderDAO
    {
        LucySalesDataContext context = new LucySalesDataContext();

        public List<Order> GetOrders()
        {
            return context.Orders.ToList();
        }

        public bool AddOrder(Order order)
        {
            try
            {
                context.Orders.Add(order);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding order: {ex.Message}");
                return false;
            }
        }

        public bool DeleteOrder(Order order)
        {
            try
            {
                context.Orders.Remove(order);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting order: {ex.Message}");
                return false;
            }
        }

        public bool UpdateOrder(Order order)
        {
            try
            {
                var existingOrder = context.Orders.Find(order.OrderId);
                if (existingOrder != null)
                {
                    existingOrder.CustomerId = order.CustomerId;
                    existingOrder.EmployeeId = order.EmployeeId;
                    existingOrder.OrderDate = order.OrderDate;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order: {ex.Message}");
                return false;
            }
        }
    }
}
