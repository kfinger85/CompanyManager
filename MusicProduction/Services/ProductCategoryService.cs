using MusicProduction.Models;

namespace MusicProduction.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly CompanyManagerContext _context;

        public ProductCategoryService(CompanyManagerContext context)
        {
            _context = context;
        }

        public ProductCategory GetByNameAsync(string name)
        {
            return  _context.ProductCategory
                .SingleOrDefault(pc => pc.Name == name);
        }

        public ProductCategory GetByIdAsync(int id)
        {
            var productCategory = _context.ProductCategory
            .SingleOrDefault(pc => pc.ProductCategoryId == id);
            if (productCategory == null)
            {
                throw new Exception($"{id} category not found");
            }
            return productCategory;
        }

        public ProductCategory GetByName(string categoryName)
        {
            string name = categoryName.Trim().ToLower(); 
            var productCategory = _context.ProductCategory
                .SingleOrDefault(pc => pc.Name.Trim().ToLower() == name);
                if (productCategory == null)
                {
                    throw new Exception($"{categoryName} category not found");
                }
            return productCategory;
        }
    }
}