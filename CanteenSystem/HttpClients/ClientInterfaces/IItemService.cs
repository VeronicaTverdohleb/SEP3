using Shared.Dtos;
using Shared.Model;

namespace HttpClients.ClientInterfaces;
/// <summary>
/// Interface used by ItemService 
/// </summary>
public interface IItemService
{
    
    Task CreateAsync(ItemCreationDto dto);
    Task<ICollection<Item>> GetAsync(
        string? name
    );
    Task<ItemBasicDto> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}