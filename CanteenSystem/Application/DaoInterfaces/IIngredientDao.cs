using Shared.Model;

namespace Application.DaoInterfaces;

public interface IIngredientDao
{
    Task<Ingredient> CreateAsync(Ingredient ingredient);
    Task AddIngredient(Ingredient ingredient); 
    Task<IEnumerable<Ingredient>> GetAsync();
    Task<Ingredient?> GetByIdAsync(int id);
    Task RemoveIngredientAmount(int value);
    Task DeleteAsync(int id);
    Task<Ingredient?> GetByNameAsync(string name);
}