namespace CompanyManager.Models
{
public class ProductCategory
{
    public int ProductCategoryId { get; set; }
    public string Name { get; set; }

    public int? ParentProductCategoryId { get; set; }
    public ProductCategory ParentProductCategory { get; set; }

    public ICollection<ProductCategory> SubCategories { get; set; }
    public ICollection<Product> Products { get; set; }
}


}