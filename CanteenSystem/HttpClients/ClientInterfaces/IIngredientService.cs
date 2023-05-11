using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IIngredientService
{
    Task<ICollection<Ingredient>> getAllIngredientsAsync(string? name);

    
}