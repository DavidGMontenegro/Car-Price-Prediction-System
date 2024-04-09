using FinalAPI.Models;
using System.Threading.Tasks;

namespace FinalAPI.Services
{
    public interface IUserService
    {
        Task<User> RegisterUser(User user);
        Task<User?> Authenticate(string username, string password);
    }
}
