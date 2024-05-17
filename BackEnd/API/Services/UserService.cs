using FinalAPI.Data;
using FinalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            this._context = context;
        }

        public async Task<User> RegisterUser(User user)
        {
            // Verificar si el usuario ya existe
            if (await _context.Users.AnyAsync(u => u.Username == user.Username || u.Email == user.Email))
            {
                throw new ArgumentException("Username or email already in use");
            }
            
            // Guardar el usuario en la base de datos
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username || u.Email == username);
            if (user == null)
            {
                return null;
            }
            if (user.Password == password)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<User?> ModifyUserData(string oldUsername, string newUsername, string newEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == oldUsername);
            if (user == null)
            {
                return null;
            }

            if (await _context.Users.AnyAsync(u => u.Username == newUsername && u.Id != user.Id))
            {
                throw new ArgumentException("New username is already in use");
            }

            if (await _context.Users.AnyAsync(u => u.Email == newEmail && u.Id != user.Id))
            {
                throw new ArgumentException("New email is already in use");
            }

            user.Username = newUsername;
            user.Email = newEmail;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserData(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        public async Task<User?> ChangeUserPassword(string username, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return null;
            }

            user.Password = newPassword;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> ChangeUserProfilePic(string username, byte[] newProfilePic)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return null;
            }

            user.ProfilePicture = newProfilePic;

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
