using MusicProduction.Models;

namespace MusicProduction.Services
{
    public interface IProductCategoryService
{
    // Other methods...
    
    Task<ProductCategory> GetByNameAsync(string name);
    Task<ProductCategory> GetByIdAsync(int id);
}
}