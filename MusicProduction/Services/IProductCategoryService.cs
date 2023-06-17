using MusicProduction.Models;

namespace MusicProduction.Services
{
    public interface IProductCategoryService
{
    // Other methods...
    
    ProductCategory GetByNameAsync(string name);
    ProductCategory GetByIdAsync(int id);

    ProductCategory GetByName(string categoryName);

}
}