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

        public async Task<List<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

public async Task<Product> CreateProduct(string make, string model, string? serialNumber, string categoryName, 
                                        string? description, decimal price, int stock, string? subCategoryName)
{

    ProductCategory category = await _productCategoryService.GetByNameAsync(categoryName);
    if(category == null){
        Logger.LogInformation($"Product category with name {categoryName} does not exist.");
        throw new Exception($"Product category with name {categoryName} does not exist."); 
        }

    ProductCategory subCategory = subCategoryName != null ? await _productCategoryService.GetByNameAsync(subCategoryName) : null;

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

    return await _productRepository.CreateProduct(product);      
}


        public async Task<Product> UpdateProduct(Product product)
        {
            return await _productRepository.UpdateProduct(product);
        }

        public async Task DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);
        }


    }
}
