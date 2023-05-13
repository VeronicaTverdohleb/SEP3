using Shared.Dtos;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IItemService
{
    
    Task CreateAsync(ItemCreationDto dto);
    Task<ICollection<Item>> GetAsync(
        string? name
    );
    Task UpdateAsync(ManageItemDto dto);
    Task<ItemBasicDto> GetByIdAsync(int id);
    Task<ItemBasicDto> GetByNameAsync(string name);
    Task DeleteAsync(int id);
}