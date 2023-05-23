using Shared.Dtos;
using Shared.Model;

namespace Application.DaoInterfaces;

public interface IMenuDao
{
    public Task<MenuBasicDto?> GetMenuByDateAsync(DateOnly date);
    public Task UpdateMenuAsync(MenuUpdateDto dto);
    public Task<Menu> CreateAsync(Menu menu);
}