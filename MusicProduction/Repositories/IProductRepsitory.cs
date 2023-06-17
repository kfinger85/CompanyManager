using MusicProduction.Models;


namespace MusicProduction.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        Product CreateProduct(Product product);
        Product UpdateProduct(Product product);
        void DeleteProduct(int id);

    }
}
