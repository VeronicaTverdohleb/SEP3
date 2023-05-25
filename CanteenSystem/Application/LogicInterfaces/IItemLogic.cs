using Shared.Dtos;
using Shared.Model;

namespace Application.LogicInterfaces;

/// <summary>
/// Interface implemented by ItemLogic
/// </summary>
public interface IItemLogic
{
    Task<Item> CreateAsync(ItemCreationDto dto);
    Task DeleteAsync(int id);
    Task<ItemBasicDto?> GetByIdAsync(int id);
    Task<ItemBasicDto?> GetByNameAsync(string name);
    Task<IEnumerable<Item>> GetAllItemsAsync(SearchItemSto searchParameters);

    
}