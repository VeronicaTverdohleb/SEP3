using Shared.Dtos;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IItemService
{
    
    Task CreateAsync(ManageItemDto dto);
    Task<ICollection<Item>> GetAsync(
        string? name, 
        int? id, 
        List<Ingredient?> ingredients, 
        string? titleContains
    );
    Task UpdateAsync(ManageItemDto dto);
    Task<ManageItemDto> GetByIdAsync(int id);
    Task<ManageItemDto> GetByNameAsync(string name);
    Task DeleteAsync(int id);
}