using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using CompanyManager.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MusicProduction.Models;


// TODO: Implement IOrderService
namespace MusicProduction.Services
{
    public class OrderService : IOrderService
    {
        private readonly CompanyManagerContext _context;

        public OrderService(CompanyManagerContext context)
        {
            _context = context;
        }

        public Order GetOrderById(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderLineItems)
                .ThenInclude(oli => oli.Product)
                .SingleOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                Logger.LogInformation($"No order found with id {id}");
                throw new ArgumentException($"No order found with id {id}");
            }

            return order;
        }

        public List<Order> GetAllOrders()
        {
            var orders = _context.Orders
                .Include(o => o.OrderLineItems)
                .ThenInclude(oli => oli.Product)
                .ToList();

            if (orders.Count == 0)
            {
                Logger.LogInformation("No orders found");
                throw new InvalidOperationException("No orders found");
            }

            return orders;
        }

    public Order CreateOrder(string customerName, DateTime startOrderDate, DateTime endOrderDate, List<Product> products)
    {
        if (string.IsNullOrWhiteSpace(customerName))
        {
            Logger.LogInformation("CustomerName is not provided.");
            throw new ArgumentException("CustomerName is not provided.");
        }

        if (products == null || products.Count == 0)
        {
            Logger.LogInformation("No products provided for the order.");
            throw new ArgumentException("No products provided for the order.");
        }

        Order order = new Order
        {
            CustomerName = customerName,
            StartOrderDate = startOrderDate,
            EndOrderDate = endOrderDate,
        };

        _context.Orders.Add(order);
        _context.SaveChanges();

        List<OrderLineItem> orderLineItems = ProductsToOrderLineItems(products, order.OrderId);


        foreach (var oli in orderLineItems)
        {
            oli.Product = _context.Product.Find(oli.Product.ProductId);

            if (oli.Product == null)
            {
                Logger.LogInformation($"Creation of {order.OrderId}: Product {oli.ProductId} not found");
                throw new InvalidOperationException($"Creation of {order.OrderId}: Product {oli.ProductId} not found");
            }
        }

    order.OrderLineItems = orderLineItems;

    _context.SaveChanges();

        return order;
    }


        public Order UpdateOrder(Order order)
        {
            if (order == null)
            {
                Logger.LogInformation("Order is null.");
                throw new ArgumentException("Order is null.");
            }

            _context.Orders.Update(order);
            _context.SaveChanges();
            
            return order;
        }

        public void DeleteOrder(int id)
        {
            Order order = _context.Orders.Find(id);

            if (order == null)
            {
                Logger.LogInformation($"No order found with id {id}");
                throw new ArgumentException($"No order found with id {id}");
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        private List<OrderLineItem> ProductsToOrderLineItems(List<Product> products, int orderId)
        {
            List<OrderLineItem> orderLineItems = new List<OrderLineItem>();
            foreach (var product in products)
            {
                orderLineItems.Add(new OrderLineItem
                {
                    Product = product,
                    ProductId = product.ProductId,
                    Quantity = 1,
                    OrderId = orderId
                });
            }
            _context.OrderLineItems.AddRange(orderLineItems);
            _context.SaveChanges();

            return orderLineItems;
        }

    }
}
