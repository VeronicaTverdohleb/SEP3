using Shared.Dtos;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IItemLogic
{
    Task<Item> CreateAsync(ItemCreationDto dto);
    Task<IEnumerable<Item>> GetAsync(SearchItemSto searchParameters);
    Task UpdateAsync(ManageItemDto post);
    Task DeleteAsync(int id);
    Task<ItemBasicDto?> GetByIdAsync(int id);
    Task<ItemBasicDto?> GetByNameAsync(string name);

    
}