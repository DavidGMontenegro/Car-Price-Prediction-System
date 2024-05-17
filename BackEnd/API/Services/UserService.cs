using FinalAPI.Data;
using FinalAPI.Models;
using FinalAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

/// <summary>
/// Service class for user-related operations.
/// </summary>
public class UserService : IUserService
{
    private readonly DataContext _context;

    /// <summary>
    /// Constructor for initializing UserService with DataContext dependency.
    /// </summary>
    /// <param name="context">Data context</param>
    public UserService(DataContext context)
    {
        this._context = context;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="user">User object</param>
    public async Task<User> RegisterUser(User user)
    {
        // Check if the user already exists
        if (await _context.Users.AnyAsync(u => u.Username == user.Username || u.Email == user.Email))
        {
            throw new ArgumentException("Username or email already in use");
        }

        // Save the user to the database
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    /// <summary>
    /// Authenticates a user.
    /// </summary>
    /// <param name="username">Username or email</param>
    /// <param name="password">Password</param>
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

    /// <summary>
    /// Modifies user data.
    /// </summary>
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

    /// <summary>
    /// Retrieves user data by username.
    /// </summary>
    public async Task<User?> GetUserData(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }

    /// <summary>
    /// Changes user password.
    /// </summary>
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

    /// <summary>
    /// Changes user profile picture.
    /// </summary>
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
