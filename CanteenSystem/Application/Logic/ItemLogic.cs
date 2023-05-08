using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace Application.Logic;

public class ItemLogic : IItemLogic
{
    private readonly IItemDao itemDao;
    private  int value=0;
    private readonly IIngredientDao ingredientDao;

    public ItemLogic(IItemDao itemDao, IIngredientDao ingredientDao)
    {
        this.itemDao = itemDao;
        this.ingredientDao = ingredientDao;
    }
    


   /* public async Task<Item> CreateAsync(ManageItemDto dto)
    {
        List<Ingredient?> ingredient = await ingredientDao(dto.ingredientId);
        if (ingredient == null)
        {
            throw new Exception($"Ingredient was not found.");

        }
        Item item = new Item(dto.name, ingredient);
        Item itemCreated = await itemDao.CreateAsync(item);
        return itemCreated;
        

    }*/


   public Task<Item> CreateAsync(ManageItemDto dto)
   {
       throw new NotImplementedException();
   }

   public Task<IEnumerable<Item>> GetAsync(ManageItemDto searchParameters)
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

        List<Ingredient>? ingredients = null;
        if (dto.Ingredients!= null)
        {
            ingredients = null;//await ingredientDao.GetByIdAsync((int)dto.ingredientId);
            if (ingredients == null)
            {
                throw new Exception($"Ingredient was not found.");

            }
        }

        List<Ingredient> ingredientToUse = ingredients ?? existing.Ingredients;
        string titleToUse = dto.name ?? existing.name;
        Item updated = new(titleToUse, ingredientToUse)
        {
            Id = existing.Id
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

    public async Task<ManageItemDto> GetByIdAsync(int id)
    {
        Item? item = await itemDao.GetByIdAsync(id);
        if (item == null)
        {
            throw new Exception($"Item with ID {id} was not found!");
        }

        return new ManageItemDto(item.name,item.Id, item.Ingredients);
    }

    public async Task<ManageItemDto> GetByNameAsync(string name)
    {
        Item? item = await itemDao.GetByNameAsync(name);
        if (item == null)
        {
            throw new Exception($"Item with ID {name} was not found!");
        }

        return new ManageItemDto(item.name, item.Id, item.Ingredients);

    }
}