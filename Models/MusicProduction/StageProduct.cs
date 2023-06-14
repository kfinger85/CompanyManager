namespace  CompanyManager.Models
{
    public class StageProduct
    {
        public int StageId { get; set; }
        public Stage Stage { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

}