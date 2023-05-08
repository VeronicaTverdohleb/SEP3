using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
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
        if (ingredient.Name == null)
        {
            throw new Exception("Ingredient requires a name!");
        }

        Ingredient todo = new Ingredient(dto.Name, dto.Amount, dto.Allergens);
        Ingredient created = await ingredientDao.CreateAsync(todo);
        return created;
    }

    public async Task AddIngredient(IngredientUpdateDto dto)
    {
        Ingredient? ingredient = await ingredientDao.GetByIdAsync(dto.Id);
        if (ingredient == null)
        {
            throw new Exception($"Ingredient with the id {dto.Id} was not found!");
        }

        int amountToUse = dto.Amount;
        Ingredient updated = new(ingredient.Name, amountToUse, ingredient.Allergens)
        {
            Id = ingredient.Id
        };

        await ingredientDao.AddIngredient(updated);
    }

    public Task<IEnumerable<Ingredient>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Ingredient> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveIngredientAmount(int value)
    {
        throw new NotImplementedException();
    }

    public Task DeleteIngredient(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Ingredient?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}