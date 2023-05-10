using Shared.Dtos;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IItemLogic
{
    Task<Item> CreateAsync(ItemCreationDto dto);
    Task<IEnumerable<Item>> GetAsync(SearchItemSto searchParameters);
    Task UpdateAsync(ManageItemDto post);
    Task DeleteAsync(int id);
    Task<ManageItemDto> GetByIdAsync(int id);
    Task<ManageItemDto> GetByNameAsync(string name);

    
}