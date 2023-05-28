using Shared.Dtos.IngredientDto;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IIngredientService
{
    Task<ICollection<Ingredient>> getAllIngredientsAsync(string? name);
    Task CreateAsync(IngredientCreationDto dto);
    Task UpdateIngredientAmount(IngredientUpdateDto dto);
    Task DeleteIngredient(int id);
}