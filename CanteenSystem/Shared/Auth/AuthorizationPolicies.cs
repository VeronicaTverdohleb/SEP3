using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
namespace Shared.Auth;


public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeCanteenEmployee", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "CanteenEmployee"));
    
            options.AddPolicy("MustBeVia", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "VIAStudent", "VIAEmployee"));
        });
    }
}