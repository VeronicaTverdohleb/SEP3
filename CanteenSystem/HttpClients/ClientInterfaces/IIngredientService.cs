using Shared.Dtos.IngredientDto;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IIngredientService
{
    Task<ICollection<Ingredient>> getAllIngredientsAsync(string? name);
    Task CreateAsync(IngredientCreationDto dto);
    Task UpdateIngredientAmount(IngredientUpdateDto dto);
    Task<IngredientBasicDto?> GetByIdAsync(int id);
    Task<IngredientBasicDto?> GetByNameAsync(string name);
    Task<Ingredient?> GetByIdAsyncFromIng(int id);
    Task<Ingredient?> GetByNameAsyncFromIng(string name);
    Task DeleteIngredient(int id);
}