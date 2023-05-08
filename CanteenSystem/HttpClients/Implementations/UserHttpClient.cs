using HttpClients.ClientInterfaces;
using Shared.Model;

namespace HttpClients.Implementations;

public class UserHttpClient:IUserService
{
    public Task<User> GetUserAsync(string userName)
    {
        throw new NotImplementedException();
    }
}