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

        public List<Product> GetProducts()
        {
            return  _context.Product.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Product.Find(id);
        }

        public Product CreateProduct(Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            _context.Product.Update(product);
            _context.SaveChangesAsync();
            return product;
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Product.Find(id);
            if (product != null)
            {
                _context.Product.Remove(product);
                _context.SaveChangesAsync();
            }
        }

    }
}
