#nullable enable

using MusicProduction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicProduction.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> CreateProduct(string make, string model, string? serialNumber, ProductCategory productCategory, 
                                                            string? description, decimal price, int stock, ProductCategories? subCategory, 
                                                            ICollection<StageProduct>? stageProducts);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
