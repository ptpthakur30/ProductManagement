using ProductManagement.DataAccess.Interface;
using ProductManagement.Utility;
using System;
using System.Threading.Tasks;

namespace ProductManagement.DataAccess.Repository
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _db;

        public UnitOfWork(DataContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            // ApplicationUser = new ApplicationUserRepository(_db);
            // SP_Call = new SP_Call(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        // public ISP_Call SP_Call { get; private set; }


        public async Task<bool> Save()
        {
          var success = await  _db.SaveChangesAsync() > 0;
            if (success)
                return true;
            throw new Exception("Problem saving changes");
        }
    }
}