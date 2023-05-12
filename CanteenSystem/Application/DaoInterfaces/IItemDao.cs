using Shared.Dtos;
using Shared.Model;

namespace Application.DaoInterfaces;

public interface IItemDao
{
    Task<Item> CreateAsync(ItemCreationDto item);
    Task<IEnumerable<Item>> GetAsync(SearchItemSto searchParameters);
    Task<IEnumerable<Item>> GetAllItemsAsync();
  
    Task DeleteAsync(int id);
    Task<Item?> GetByNameAsync(string name);
    Task<Item?> GetByIdAsync(int id);
    
    
    

}