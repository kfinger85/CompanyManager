namespace  MusicProduction.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ProductCategories SubCategory { get; set; }
        public ICollection<StageProduct> StageProducts { get; set; }
    }
}