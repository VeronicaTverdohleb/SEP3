using System.Security.Claims;

namespace BlazorApp.Services;

/// <summary>
/// Interface implemented by AuthService in Http package
/// </summary>
public interface IAuthService
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}