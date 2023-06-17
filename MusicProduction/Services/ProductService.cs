#nullable enable

using MusicProduction.Models;
using MusicProduction.Repositories;
using MusicProduction.Services;

using CompanyManager.Logging; 

namespace CompanyManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
    private readonly IProductCategoryService _productCategoryService;


        public ProductService(IProductRepository productRepository, IProductCategoryService productCategoryService)
        {
            _productRepository = productRepository;
            _productCategoryService = productCategoryService;

        }

        public List<Product> GetProducts()
        {
            return  _productRepository.GetProducts();
        }

        public  Product GetProductById(int id)
        {
            return  _productRepository.GetProductById(id);
        }



public  Product CreateProduct(string make, string model, string? serialNumber, string categoryName, 
                                        string? description, decimal price, int stock, string? subCategoryName)
{

    ProductCategory category =  _productCategoryService.GetByName(categoryName);
    if(category == null){
        Logger.LogInformation($"Product category with name {categoryName} does not exist.");
        throw new Exception($"Product category with name {categoryName} does not exist."); 
        }

    ProductCategory subCategory = subCategoryName != null ?  _productCategoryService.GetByName(subCategoryName) : null;

    Product product = new Product
    {
        Make = make,
        Model = model,
        SerialNumber = serialNumber,
        Description = description,
        Price = price,
        Stock = stock,
        ParentProductCategoryId = category.ProductCategoryId,
        SubProductCategoryId = subCategory?.ProductCategoryId,  // it will be null if subCategory is null
    };

    return  _productRepository.CreateProduct(product);      
}


        public Product UpdateProduct(Product product)
        {
            return  _productRepository.UpdateProduct(product);
        }

        public void DeleteProduct(int id)
        {
             _productRepository.DeleteProduct(id);
        }


    }
}
