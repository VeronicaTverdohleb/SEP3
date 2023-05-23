using Shared.Dtos;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

/// <summary>
/// Interface implemented by MenuService
/// </summary>
public interface IMenuService
{
    public Task<MenuBasicDto> GetMenuByDateAsync(DateOnly date);

    public Task UpdateMenuAsync(MenuUpdateDto dto);
    public Task CreateAsync(MenuBasicDto dto);
}