using System.Collections.Generic;
using System.Threading.Tasks;
using MusicProduction.Models;

namespace MusicProduction.Services
{
    public interface IOrderService
    {
        Order GetOrderById(int id);
        List<Order> GetAllOrders();
        Order CreateOrder(string customerName, DateTime startOrderDate, DateTime endOrderDate, List<Product> products);
        Order UpdateOrder(Order order);
        void DeleteOrder(int id);
    }
}
