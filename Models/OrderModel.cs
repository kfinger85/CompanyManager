
namespace CompanyManager.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }

        // Navigation property
        public ICollection<OrderLineItem> OrderLineItems { get; set; }
    }

}