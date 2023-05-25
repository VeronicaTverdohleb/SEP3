using Shared.Model;

namespace WebAPI.Services;

/// <summary>
/// Interface implemented by AuthService
/// </summary>
public interface IAuthService
{
    Task<User> ValidateUser(string username, string password);
}