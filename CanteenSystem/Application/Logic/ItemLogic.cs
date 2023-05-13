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
        // Missing logic 
        // Does the ingredient actually exist? And if yes, do we have enough to make the item?
        // Can this ingredient go on this type of product (pineapple on pizza)
        foreach (int ingredientId in dto.IngredientIds)
        {
            Ingredient? existing = await ingredientDao.GetByIdAsync(ingredientId);
            if (existing == null)
            {
                throw new Exception($"This ingredients you try to use, does not exist!");
            }

            if (existing.Amount == 0)
            {
                throw new Exception($"There is not enough of this ingredient to make this item");
            }
        }
        
        Item itemCreated = await itemDao.CreateAsync(dto);
        return itemCreated;
    }


    public Task<IEnumerable<Item>> GetAsync(SearchItemSto searchParameters)
    {
        return itemDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(ManageItemDto dto)
    {
        Item? existing = await itemDao.GetByIdAsync(dto.Id);
        if (existing == null)
        {
            throw new Exception($"Item with ID {dto.Id} not found!");
        }

        ICollection<Ingredient?> ingredients = new List<Ingredient?>();
        if (dto.Ingredients != null)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                Ingredient? ingredient = await ingredientDao.GetByIdAsync(dto.ingredientId);
                ingredients.Add(ingredient);
            }
        }

        if (ingredients == null)
        {
            throw new Exception($"Ingredient was not found.");
        }


        ICollection<Ingredient> ingredientToUse = dto.Ingredients ?? existing.Ingredients;
        string titleToUse = dto.name ?? existing.Name;
        int priceToUse = existing.Price;
        Item updated = new()
        {
            Name = titleToUse,
            Ingredients = { ingredientToUse.GetEnumerator().Current },
            Id = existing.Id,
            Price = existing.Price
        };

        await itemDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Item? item = await itemDao.GetByIdAsync(id);
        if (item == null)
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

        return new ItemBasicDto(item.Name, item.Price,item.Ingredients);
    }

    public async Task<ItemBasicDto?> GetByNameAsync(string name)
    {
        Item? item = await itemDao.GetByNameAsync(name);
        if (item == null)
        {
            throw new Exception($"Item with Name {name} was not found!");
        }

        return new ItemBasicDto(item.Name, item.Price,item.Ingredients);
    }

    public Task<IEnumerable<Item>> GetAllItemsAsync()
    {
        return itemDao.GetAllItemsAsync();
    }
}