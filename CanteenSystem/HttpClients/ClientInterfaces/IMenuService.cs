using Shared.Dtos;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IMenuService
{
    public Task<MenuBasicDto> GetMenuByDateAsync(DateTime date);

}