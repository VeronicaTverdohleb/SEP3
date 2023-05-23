using System.Diagnostics.Tracing;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Dtos.IngredientDto;
using Shared.Model;

namespace Application.Logic;

public class ItemLogic : IItemLogic
{
    private readonly IItemDao itemDao;

    // private  int value=0;
    private readonly IIngredientDao ingredientDao;


    public ItemLogic(IItemDao itemDao, IIngredientDao ingredientDao)
    {
        this.itemDao = itemDao;
        this.ingredientDao = ingredientDao;
    }


    public async Task<Item> CreateAsync(ItemCreationDto dto)
    {
        foreach (int ingredientId in dto.IngredientIds)
        {
            Ingredient? existing = await ingredientDao.GetByIdAsync(ingredientId);
            if (existing is { Name: null })
            {
                throw new Exception($"This ingredient you try to use, does not exist!");
            }

            if (existing is { Amount: 0 })
            {
                throw new Exception($"There is not enough of this ingredient to make this item");
            }
        }

        Item? existingItem = await itemDao.GetByNameAsync(dto.Name);
        if (existingItem?.Name != null)
        {
            throw new Exception("An item with this name already exists");
        }

        if (dto.IngredientIds == null || !dto.IngredientIds.Any())
        {
            throw new Exception("An item needs to have ingredients");
        }

        Item itemCreated = await itemDao.CreateAsync(dto);

        return itemCreated;
    }


    public async Task DeleteAsync(int id)
    {
        Item? item = await itemDao.GetByIdAsync(id);
        if (item is {Id:0})
        {
            throw new Exception($"Item with ID {id} not found!");

        }
        if (item== null)
        {
            throw new Exception($"Item with ID {id} was not found!");
        }

        

        await itemDao.DeleteAsync(id);
    }

    public async Task<ItemBasicDto?> GetByIdAsync(int id)
    {
        Item? item = await itemDao.GetByIdAsync(id);
        if (item == null)
        {
            throw new Exception($"Item with ID {id} was not found!");
        }

        return new ItemBasicDto(item.Name, item.Price, item.Ingredients);
    }

    public async Task<ItemBasicDto?> GetByNameAsync(string name)
    {
        Item? item = await itemDao.GetByNameAsync(name);
        if (item == null)
        {
            throw new Exception($"Item with Name {name} was not found!");
        }

        return new ItemBasicDto(item.Name, item.Price, item.Ingredients);
    }

    public Task<IEnumerable<Item>> GetAllItemsAsync(SearchItemSto searchParameters)
    {
        return itemDao.GetAllItemsAsync(searchParameters);
    }
}