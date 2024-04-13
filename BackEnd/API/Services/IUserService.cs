using FinalAPI.Models;
using System.Threading.Tasks;

namespace FinalAPI.Services
{
    public interface IUserService
    {
        Task<User> RegisterUser(User user);
        Task<User?> Authenticate(string username, string password);
        Task<User?> ModifyUserData(string oldUsername, string newUsername, string newEmail);
        Task<User?> GetUserData(string username);
        Task<User?> ChangeUserPassword(string username, string oldPassword, string newPassword);
        Task<User?> ChangeUserProfilePic(string username, byte[] newProfilePic);
    }
}
