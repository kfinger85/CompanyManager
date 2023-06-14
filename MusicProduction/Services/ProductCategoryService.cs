using MusicProduction.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicProduction.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly CompanyManagerContext _context;

        public ProductCategoryService(CompanyManagerContext context)
        {
            _context = context;
        }

        public async Task<ProductCategory> GetByNameAsync(string name)
        {
            return await _context.ProductCategory
                .SingleOrDefaultAsync(pc => pc.Name == name);
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await _context.ProductCategory
                .SingleOrDefaultAsync(pc => pc.ProductCategoryId == id);
        }
    }
}