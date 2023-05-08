using Shared.Dtos;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IItemLogic
{
    Task<Item> CreateAsync(ManageItemDto dto);
    Task<IEnumerable<Item>> GetAsync(ManageItemDto searchParameters);
    Task UpdateAsync(ManageItemDto post);
    Task DeleteAsync(int id);
    Task<ManageItemDto> GetByIdAsync(int id);
    Task<ManageItemDto> GetByNameAsync(string name);

    
}