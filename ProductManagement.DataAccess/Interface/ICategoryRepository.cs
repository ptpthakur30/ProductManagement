using ProductManagement.Models;

namespace ProductManagement.DataAccess.Interface
{
    public interface ICategoryRepository : IRepositoryAsync<Category>
    {
        void Update(Category category);
    }
}