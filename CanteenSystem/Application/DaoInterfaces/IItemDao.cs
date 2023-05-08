using Shared.Dtos;
using Shared.Model;

namespace Application.DaoInterfaces;

public interface IItemDao
{
    Task<Item> CreateAsync(Item item);
    Task<IEnumerable<Item>> GetAsync(ManageItemDto searchParameters);

    Task UpdateAsync(Item item);
    Task DeleteAsync(int id);
    Task<Item?> GetByNameAsync(string name);
    Task<Item?> GetByIdAsync(int id);
    
    

}