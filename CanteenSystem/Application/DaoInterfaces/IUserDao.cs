using Shared.Model;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    public Task<User> GetByUsernameAsync(string username);

}