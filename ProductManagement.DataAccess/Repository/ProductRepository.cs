using System.Linq;
using ProductManagement.DataAccess.Interface;
using ProductManagement.Models;
using ProductManagement.Utility;

namespace ProductManagement.DataAccess.Repository
{
   public class ProductRepository : RepositoryAsync<Product>, IProductRepository
    {
        private readonly DataContext _db;

        public ProductRepository(DataContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(s => s.Id == product.Id);
            if (objFromDb == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { product = "Not Found" });
            if (objFromDb != null)
            {
                //if (product.ImageUrl != null)
                //{
                //    objFromDb.ImageUrl = product.ImageUrl;
                //}
                objFromDb.Price = product.Price;
                objFromDb.Title = product.Title;
                objFromDb.Description = product.Description;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.Author = product.Author;
            }
        }
    }
}