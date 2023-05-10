using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
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
       ICollection<Ingredient> ingredients = dto.Ingredients;
       for (int i = 0; i < ingredients.Count; i++)
       {
           Ingredient? ingredient = await ingredientDao.GetByIdAsync(dto.ingredientId);
           if (ingredient == null)
           {
               throw new Exception($"Ingredient was not found.");

           }

           if (ingredient.Id.Equals(dto.ingredientId))
           {
               //dto.Ingredients.Coun
           }
       }
       


       Item item = new Item(dto.name, ingredients);
       Item itemCreated = await itemDao.CreateAsync(item);
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
        

        ICollection<Ingredient> ingredientToUse = ingredients ?? existing.Ingredients;
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

        return new ManageItemDto(item.name,item.Id, item.Ingredients.Count);
    }

    public async Task<ManageItemDto> GetByNameAsync(string name)
    {
        Item? item = await itemDao.GetByNameAsync(name);
        if (item == null)
        {
            throw new Exception($"Item with ID {name} was not found!");
        }

        return new ManageItemDto(item.name, item.Id, item.Ingredients.Count);

    }
}