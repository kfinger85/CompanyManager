namespace CompanyManager.Models
{
    public class OrderLineItem
    {
        public int OrderLineItemId { get; set; }
        public int OrderId { get; set; }
        public int InstrumentId { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }

}