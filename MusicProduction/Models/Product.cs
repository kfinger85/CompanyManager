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

    public int ParentProductCategoryId { get; set; }
    public int? SubProductCategoryId { get; set; }

    public ProductCategory ParentProductCategory { get; set; }
    public ProductCategory SubProductCategory { get; set; }




    public Product()
    {
    }
    }


}