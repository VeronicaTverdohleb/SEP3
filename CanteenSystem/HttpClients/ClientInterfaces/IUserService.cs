using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> GetUserAsync(string userName);
}