using ProductManagement.Models;
using System.Threading.Tasks;

namespace ProductManagement.DataAccess.Interface
{
    public interface IRegisterRepository
    {
        Task<bool> RegisterUser(Register registerDetails);
    }
}