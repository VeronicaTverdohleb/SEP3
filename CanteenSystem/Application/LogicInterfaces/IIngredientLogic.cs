using Shared.Dtos;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IIngredientLogic
{
    Task<Ingredient> CreateAsync(IngredientCreationDto dto);
    Task AddIngredient(IngredientUpdateDto dto);
    Task<IEnumerable<Ingredient>> GetAsync();
    Task<Ingredient?> GetByIdAsync(int id);
    Task<Ingredient?> GetByNameAsync(string name);
    Task RemoveIngredientAmount(int value);
    Task DeleteIngredient(int id);
}