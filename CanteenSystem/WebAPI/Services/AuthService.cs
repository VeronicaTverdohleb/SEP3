using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Shared.Model;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserDao users;

    public AuthService(IUserDao users)
    {
        this.users = users;
    }

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
