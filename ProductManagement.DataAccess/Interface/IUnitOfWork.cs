using System;
using System.Threading.Tasks;

namespace ProductManagement.DataAccess.Interface
{
    public interface IUnitOfWork
    {
         ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        
        // IApplicationUserRepository ApplicationUser { get; }
        // ISP_Call SP_Call { get; }
        Task<bool> Save();
       
    }
}