using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Model;

namespace EfcDataAccess.DAOs;

public class 
    IngredientDao : IIngredientDao
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

    public async Task UpdateAsync(Ingredient ingredient)
    {
        context.Ingredients.Update(ingredient);
        await context.SaveChangesAsync();
    }

    public Task<IEnumerable<Ingredient>> GetAsync()
    {
        IEnumerable<Ingredient> list = context.Ingredients.ToList();
        return Task.FromResult(list);
    }

    public async Task<Ingredient?> GetByIdAsync(int id)
    {
        Ingredient? found = await context.Ingredients
            .AsNoTracking()
            .SingleOrDefaultAsync(post => post.Id == id);
        if (found == null)
        {
            throw new Exception($"Ingredient with id {id} not found");
        }
        return found;
    }
    
    public async Task DeleteAsync(int id)
    {
        Ingredient? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Ingredient with id {id} not found");
        }

        context.Ingredients.Remove(existing);
        await context.SaveChangesAsync();

    }

    public async Task<Ingredient?> GetByNameAsync(string name)
    {
        var found = await context.Ingredients
            .AsNoTracking().SingleOrDefaultAsync(i => i.Name == name);
        return found;
    }
}