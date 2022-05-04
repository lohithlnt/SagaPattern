using OrderService.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public interface IOrderDataAccess
    {
        List<Order> GetAllOrder();
        void SaveOrder(Order order);
        Order GetOrder(Guid orderId);
        Task<bool> DeleteOrder(Guid orderId);
    }
}
