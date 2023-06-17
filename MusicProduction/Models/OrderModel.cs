
using Bogus.DataSets;

namespace MusicProduction.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public DateTime StartOrderDate { get; set; }

        public DateTime EndOrderDate { get; set; }

        public DateTime OrderCreated { get; set; }
        public DateTime? OrderUpdated { get; set; }

        // Navigation property
        public ICollection<OrderLineItem> OrderLineItems { get; set; }
        public Order()
        {
            OrderLineItems = new List<OrderLineItem>();
            OrderCreated = DateTime.Now;
            OrderUpdated = null;
        }

    }

    

}