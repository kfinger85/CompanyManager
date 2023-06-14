namespace MusicProduction.Models
{
public class ProductCategory
{
    public int ProductCategoryId { get; set; }
    public string Name { get; set; }
    public int? ParentProductCategoryId { get; set; }


    /*
    If all ParentProductCategoryId values are NULL, that means every category in your database 
    is currently considered a top-level category, with no parent category. 
    */
    public ProductCategory ParentProductCategory { get; set; }
    public ICollection<ProductCategory> SubCategories { get; set; }
    public ICollection<Product> Products { get; set; }  // For ParentProductCategory relationship
    public ICollection<Product> SubCategoryProducts { get; set; }  // For SubProductCategory relationship

    public ProductCategory()
    {
        SubCategories = new HashSet<ProductCategory>();
        Products = new HashSet<Product>();
        SubCategoryProducts = new HashSet<Product>();  // Initialize it
    }
}




}