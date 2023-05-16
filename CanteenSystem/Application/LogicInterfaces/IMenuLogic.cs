using Shared.Dtos;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IMenuLogic
{
    public Task<MenuBasicDto> GetMenuByDateAsync(DateOnly date);
    public Task UpdateMenuAsync(MenuUpdateDto dto);
    public Task<Menu> CreateAsync(MenuBasicDto dto);
    
}