using System.ComponentModel.DataAnnotations;
using Shared.Model;

namespace WebAPI.Services;

public class AuthService : IAuthService
{

    private readonly IList<Employee> users = new List<Employee>();


    public Task<Employee> ValidateEmployee(string username, string password)
    {
        Employee? existingUser = users.FirstOrDefault(u =>
            u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public Task RegisterEmployee(Employee user)
    {

        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here

        // save to persistence instead of list

        users.Add(user);

        return Task.CompletedTask;
    }
}
