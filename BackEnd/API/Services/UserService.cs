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
                throw new ArgumentException("El usuario o correo electrónico ya están registrados.");
            }

            // Hash de la contraseña (debes implementar correctamente el hash de la contraseña)
            // user.Password = hashFunction(user.Password);

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
    }
}
