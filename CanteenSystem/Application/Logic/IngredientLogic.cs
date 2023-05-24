using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.IngredientDto;
using Shared.Model;

namespace Application.Logic;
/// <summary>
/// This class is the logical implementation for IIngredientLogic
/// </summary>
public class IngredientLogic : IIngredientLogic
{
    private readonly IIngredientDao ingredientDao;

/// <summary>
/// Instantiates the IIngredientDao in the constructor
/// </summary>
/// <param name="ingredientDao"></param>
    public IngredientLogic(IIngredientDao ingredientDao)
    {
        this.ingredientDao = ingredientDao;
    }
/// <summary>
/// Creates a new Ingredient
/// </summary>
/// <param name="dto"></param>
/// <returns>Returns the created Ingredient</returns>
/// <exception cref="Exception"></exception>
    public async Task<Ingredient> CreateAsync(IngredientCreationDto dto)
    {
        Ingredient? ingredient = await ingredientDao.GetByNameAsync(dto.Name);
        if (ingredient != null)
        {
            throw new Exception("Ingredient already exists!");
        }
        if (string.IsNullOrEmpty(dto.Name))
        {
            throw new Exception("Name Field Is Required");
        }
        if (dto.Name.Length > 50)
        {
            throw new Exception("Max Name Length Is 50 Characters");
        }

        Ingredient todo = new Ingredient(dto.Name, dto.Amount, dto.Allergen); 
        Ingredient created = await ingredientDao.CreateAsync(todo); 
        
        return created;
    }
    /// <summary>
    /// Updates an existing ingredients amount value
    /// </summary>
    /// <param name="dto"></param>
    /// <exception cref="Exception"></exception>
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
    /// <summary>
    /// Returns a list of all ingredients
    /// </summary>
    /// <returns>Returns All Ingredients</returns>
    public Task<IEnumerable<Ingredient>> GetAsync()
    {
        return ingredientDao.GetAsync();
    }
    /// <summary>
    /// Gets an ingredient by its id value
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns a new IngredientBasicDto</returns>
    /// <exception cref="Exception"></exception>
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
    /// <summary>
    /// Deletes an ingredient based on its Id value
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public async Task DeleteIngredient(int id)
    {
        Ingredient? todo = await ingredientDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Post with ID {id} was not found!");
        }
        
        await ingredientDao.DeleteAsync(id);
    }

   
    /// <summary>
    /// Gets an IngredientBasicDto by a string name
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Returns a IngredientBasicDto</returns>
    /// <exception cref="Exception"></exception>
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