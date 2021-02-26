using System.Linq;
using ProductManagement.DataAccess.Interface;
using ProductManagement.Models;
using ProductManagement.Utility;

namespace ProductManagement.DataAccess.Repository
{
     public class CategoryRepository : RepositoryAsync<Category>, ICategoryRepository
    {
        private readonly DataContext _db;

        public CategoryRepository(DataContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var objFromDb = _db.Categories.FirstOrDefault(s => s.Id == category.Id);
            if (objFromDb == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { Category = "Not Found" });
            else
            {
                objFromDb.Name = category.Name;
               
            }
        }
    }
}