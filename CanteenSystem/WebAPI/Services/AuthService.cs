using Application.DaoInterfaces;
using Shared.Model;

namespace WebAPI.Services;

/// <summary>
/// Class that serves the same purpose as the other DAOs - it uses the Database Context
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUserDao users;

    public AuthService(IUserDao users)
    {
        this.users = users;
    }

    /// <summary>
    /// Method that validates the User login credentials based on the database info
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await users.GetByUsernameAsync(username);

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }
}
