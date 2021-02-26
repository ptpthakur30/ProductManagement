using ProductManagement.Models;

namespace ProductManagement.DataAccess.Interface
{
   public interface IProductRepository : IRepositoryAsync<Product>
    {
        void Update(Product product);
    }
}