using Shared.Dtos;
using Shared.Model;

namespace Application.DaoInterfaces;

/// <summary>
/// Interface implemented by MenuDao
/// </summary>
public interface IMenuDao
{
    public Task<MenuBasicDto?> GetMenuByDateAsync(DateOnly date);
    public Task UpdateMenuAsync(MenuUpdateDto dto);
    public Task<Menu> CreateAsync(Menu menu);
}