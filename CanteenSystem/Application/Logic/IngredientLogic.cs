using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Dtos.IngredientDto;
using Shared.Model;

namespace Application.Logic;

public class IngredientLogic : IIngredientLogic
{
    private readonly IIngredientDao ingredientDao;


    public IngredientLogic(IIngredientDao ingredientDao)
    {
        this.ingredientDao = ingredientDao;
    }

    public async Task<Ingredient> CreateAsync(IngredientCreationDto dto)
    {
        Ingredient? ingredient = await ingredientDao.GetByNameAsync(dto.Name);
        if (ingredient != null)
        {
            throw new Exception("Ingredient already exists!");
        }

        Ingredient todo = new Ingredient(dto.Name, dto.Amount, dto.Allergen); 
        Ingredient created = await ingredientDao.CreateAsync(todo); 
        
        return created;
    }

    public async Task UpdateIngredientAmount(IngredientUpdateDto dto)
    {
        Ingredient? ingredient = await ingredientDao.GetByIdAsync(dto.Id);
        if (ingredient == null)
        {
            throw new Exception($"Ingredient with the id {dto.Id} was not found!");
        }

        int amountToUse = dto.Amount;
        Ingredient updated = new(ingredient.Name, amountToUse, ingredient.Allergen)
        {
            Id = ingredient.Id
        };

        await ingredientDao.UpdateAsync(updated);
    }

    public Task<IEnumerable<Ingredient>> GetAsync()
    {
        return ingredientDao.GetAsync();
    }

    public async Task<IngredientBasicDto?> GetByIdAsync(int id)
    {
        Ingredient? todoIngredient = await ingredientDao.GetByIdAsync(id);
        if (todoIngredient == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        return new IngredientBasicDto(todoIngredient.Id ,todoIngredient.Name, todoIngredient.Amount, todoIngredient.Allergen);

    }
    public async Task<Ingredient?> GetByIdAsyncFromIng(int id)
    {
        Ingredient? todoIngredient = await ingredientDao.GetByIdAsync(id);
        if (todoIngredient == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        return new Ingredient(todoIngredient.Name, todoIngredient.Amount, todoIngredient.Allergen);

        
    }

    public async Task<Ingredient?> GetByNameAsyncFromIng(string name)
    {
        Ingredient? todoIngredient = await ingredientDao.GetByNameAsync(name);
        if (todoIngredient == null)
        {
            throw new Exception($"Post with id {name} not found");
        }

        return new Ingredient(todoIngredient.Name, todoIngredient.Amount, todoIngredient.Allergen);

    
    }

    public async Task DeleteIngredient(int id)
    {
        Ingredient? todo = await ingredientDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Post with ID {id} was not found!");
        }
        
        await ingredientDao.DeleteAsync(id);
    }

   

    public async Task<IngredientBasicDto?> GetByNameAsync(string name)
    {
        Ingredient? todoIngredient = await ingredientDao.GetByNameAsync(name);
        if (todoIngredient == null)
        {
            throw new Exception($"Ingredient with name {name} not found");
        }

        return new IngredientBasicDto(todoIngredient.Id ,todoIngredient.Name, todoIngredient.Amount, todoIngredient.Allergen);

    }

   
}