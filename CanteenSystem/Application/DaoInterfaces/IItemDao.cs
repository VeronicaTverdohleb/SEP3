using Shared.Dtos;
using Shared.Model;

namespace Application.DaoInterfaces;

/// <summary>
/// Interface implemented by ItemDao
/// </summary>
public interface IItemDao
{
    Task<Item> CreateAsync(ItemCreationDto item);
    Task<IEnumerable<Item>> GetAllItemsAsync(SearchItemSto searchParameters);
  
    Task DeleteAsync(int id);
    Task<Item?> GetByNameAsync(string name);
    Task<Item?> GetByIdAsync(int id);
    
    
    

}