using Shared.Dtos;
using Shared.Model;

namespace Application.DaoInterfaces;

public interface IMenuDao
{
    public Task<MenuBasicDto> GetMenuByDateAsync(DateTime date);

}