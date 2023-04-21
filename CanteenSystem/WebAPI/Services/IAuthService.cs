using Shared.Model;

namespace WebAPI.Services;

public interface IAuthService
{
    Task<Employee> ValidateEmployee(string username, string password);
    Task RegisterEmployee(Employee user); 
}