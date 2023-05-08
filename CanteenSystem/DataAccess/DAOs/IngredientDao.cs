using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Model;

namespace EfcDataAccess.DAOs;

public class IngredientDao : IIngredientDao
{
    private readonly DataContext context;
    
    public IngredientDao(DataContext context)
    {
        this.context = context;
    }
    
    public async Task<Ingredient> CreateAsync(Ingredient ingredient)
    {
        EntityEntry<Ingredient> added = await context.Ingredients.AddAsync(ingredient);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task AddIngredient(Ingredient ingredient)
    {
        context.Ingredients.Update(ingredient);
        await context.SaveChangesAsync();
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

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Ingredient?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}