using Shared.Dtos;
using Shared.Dtos.IngredientDto;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IIngredientLogic
{
    Task<Ingredient> CreateAsync(IngredientCreationDto dto);
    Task UpdateIngredientAmount(IngredientUpdateDto dto);
    Task<IEnumerable<Ingredient>> GetAsync();
    Task<IngredientBasicDto?> GetByIdAsync(int id);
    Task<IngredientBasicDto?> GetByNameAsync(string name);
    Task DeleteIngredient(int id);
    
}