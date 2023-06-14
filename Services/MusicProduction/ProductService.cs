using CompanyManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Services
{
    public class ProductService : IProductService
    {
        private readonly CompanyManagerContext _context;

        public ProductService(CompanyManagerContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProduct()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Product.FindAsync(id);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<Product>> GetProducts()
        {
            return _context.Product.ToListAsync();
        }
    }
}
