#nullable enable

using MusicProduction.Models;
using MusicProduction.Repositories;
using MusicProduction.Services;

namespace CompanyManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<Product> CreateProduct(string make, string model, string? serialNumber, ProductCategory productCategory, 
                                                            string? description, decimal price, int stock, ProductCategories? subCategory, 
                                                            ICollection<StageProduct>? stageProducts)
        {
            Product product = new Product{
                Make = make,
                Model = model,
                SerialNumber = serialNumber,
                Description = description,
                Price = price,
                Stock = stock,
                ProductCategoryId = productCategory.ProductCategoryId,
                ProductCategory = productCategory,
                SubCategory = subCategory.Value,
                StageProducts = stageProducts
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
