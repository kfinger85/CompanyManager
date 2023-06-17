#nullable enable

using MusicProduction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicProduction.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        Product CreateProduct(string make, string model, string? serialNumber, string categoryName, 
                                        string? description, decimal price, int stock, string? subCategoryName);
        Product UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
