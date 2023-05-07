using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Model;

namespace EfcDataAccess.DAOs;

public class UserDao : IUserDao
{
    private readonly DataContext context;
    
    public UserDao(DataContext context)
    {
        this.context = context;
    }


    public async Task<User> GetByUsernameAsync(string username)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.UserName.ToLower().Equals(username.ToLower())
        );
        return existing;
    }
}