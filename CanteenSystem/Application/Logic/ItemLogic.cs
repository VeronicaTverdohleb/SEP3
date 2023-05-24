using System.Diagnostics.Tracing;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Dtos.IngredientDto;
using Shared.Model;

namespace Application.Logic;

public class ItemLogic : IItemLogic
{
    /// <summary>
    /// Interface of the item Dao
    /// </summary>
    private readonly IItemDao itemDao;

    /// <summary>
    /// Interface of the ingredient Dao
    /// </summary>
    private readonly IIngredientDao ingredientDao;


    /// <summary>
    /// Constructor for the Item logic
    /// </summary>
    /// <param name="itemDao">Instantiating the interface of the Item Dao</param>
    /// <param name="ingredientDao">Instantiating the interface of the ingredient Dao</param>
    public ItemLogic(IItemDao itemDao, IIngredientDao ingredientDao)
    {
        this.itemDao = itemDao;
        this.ingredientDao = ingredientDao;
    }

/// <summary>
/// Create method which returns a new Item
/// Checks if there are ingredients to create the Item, if an Item with
/// the same name exists and if the user is trying to create an Item without ingredients
/// </summary> 
/// <param name="dto">Takes the ItemCreationDto</param>
/// <returns>a new Item</returns>
/// <exception cref="Exception"></exception>
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

/// <summary>
/// Delete method which checks if the id of the Item selected exists and if yes it gets removed
/// </summary>
/// <param name="id">Id of the selected Item</param>
/// <exception cref="Exception">Appears when no Item has the selected id</exception>
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

/// <summary>
/// Gets an existing Item by the inputted id
/// </summary>
/// <param name="id">Id of the Item</param>
/// <returns>Item which has the inputted id</returns>
/// <exception cref="Exception">Appears when no Item with the given id exists</exception>
    public async Task<ItemBasicDto?> GetByIdAsync(int id)
    {
        Item? item = await itemDao.GetByIdAsync(id);
        if (item == null)
        {
            throw new Exception($"Item with ID {id} was not found!");
        }

        return new ItemBasicDto(item.Name, item.Price, item.Ingredients);
    }

/// <summary>
/// Gets an existing Item by the inputted name
/// </summary>
/// <param name="name">Name of the Item</param>
/// <returns>Item which has the inputted name</returns>
/// <exception cref="Exception">Appears when no Item with the given name exists</exception>
    public async Task<ItemBasicDto?> GetByNameAsync(string name)
    {
        Item? item = await itemDao.GetByNameAsync(name);
        if (item == null)
        {
            throw new Exception($"Item with Name {name} was not found!");
        }

        return new ItemBasicDto(item.Name, item.Price, item.Ingredients);
    }

/// <summary>
/// Gets all the existing Items
/// </summary>
/// <param name="searchParameters">The user can find all Items with this parameter</param>
/// <returns>All Items which correspond to the parameter</returns>
    public Task<IEnumerable<Item>> GetAllItemsAsync(SearchItemSto searchParameters)
    {
        return itemDao.GetAllItemsAsync(searchParameters);
    }
}