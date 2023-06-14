using MusicProduction.Models;
using Microsoft.EntityFrameworkCore;


namespace MusicProduction.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CompanyManagerContext _context;

        public ProductRepository(CompanyManagerContext context)
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
        public void CreateProduct(string make, string model, string serialNumber, 
        string description, decimal price, int stock, int productCategoryId, 
        ProductCategory productCategory, 
        ProductCategories subCategory, 
        ICollection<StageProduct> stageProducts)
        {
            Product product = new Product();
            product.Make = make;
            product.Model = model;
            product.SerialNumber = serialNumber;
            product.Description = description;
            product.Price = price;
            product.Stock = stock;
            product.ProductCategoryId = productCategoryId;
            product.ProductCategory = productCategory;
            product.SubCategory = subCategory;
            product.StageProducts = stageProducts;
        }

    }
}
