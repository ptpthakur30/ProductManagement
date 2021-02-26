using ProductManagement.Models;
using ProductManagement.Utility;
using System.Threading.Tasks;

namespace ProductManagement.DataAccess.Interface
{
    public interface ILoginRepository
    {
        Task<UserDTO> LoginUser(Login loginDetails);
    }
}