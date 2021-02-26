using ProductManagement.Models;
using System.Threading.Tasks;

namespace ProductManagement.Business
{
    public interface IProductBusiness
    {
        Task<ProductDTO> GetProductById(int id);
    }
}