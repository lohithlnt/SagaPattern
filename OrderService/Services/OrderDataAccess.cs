using OrderService.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public class OrderDataAccess : IOrderDataAccess
    {
        OrderDbContext context;
        public OrderDataAccess(OrderDbContext _context)
        {
            context = _context;
        }
        public List<Order> GetAllOrder()
        {
            return context.Orders.ToList();
        }
        public void SaveOrder(Order order)
        {
            context.Add(order);
            context.SaveChanges();
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            Order order = context.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();

            if (order != null)
            {
                context.Remove(order);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Order GetOrder(Guid orderId)
        {
            return context.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();
        }
    }
}
