using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Model;

namespace EfcDataAccess.DAOs;
/// <summary>
/// This class is used to interact with the Sqlite database
/// </summary>
public class 
    IngredientDao : IIngredientDao
{
    private readonly DataContext context;
    /// <summary>
    /// This constructor instantiates the database
    /// </summary>
    /// <param name="context">database context</param>
    public IngredientDao(DataContext context)
    {
        this.context = context;
    }
    /// <summary>
    /// This method adds an ingredient to the database
    /// </summary>
    /// <param name="ingredient">takes in an ingredient object</param>
    /// <returns>returns the </returns>
    public async Task<Ingredient> CreateAsync(Ingredient ingredient)
    {
        EntityEntry<Ingredient> added = await context.Ingredients.AddAsync(ingredient);
        await context.SaveChangesAsync();
        return added.Entity;
    }
    /// <summary>
    /// This method updates an ingredient amount in the database
    /// </summary>
    /// <param name="ingredient">takes in an ingredient object</param>
    public async Task UpdateAsync(Ingredient ingredient)
    {
        context.Ingredients.Update(ingredient);
        await context.SaveChangesAsync();
    }
    /// <summary>
    /// This method gets all ingredients in the database
    /// </summary>
    /// <returns>returns a list of all ingredients</returns>
    public Task<IEnumerable<Ingredient>> GetAsync()
    {
        IEnumerable<Ingredient> list = context.Ingredients.ToList();
        return Task.FromResult(list);
    }
    /// <summary>
    /// This method gets an ingredient by id from the database
    /// </summary>
    /// <param name="id">an integer for id</param>
    /// <returns>returns the ingredient found</returns>
    /// <exception cref="Exception"></exception>
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
    /// <summary>
    /// This method deletes an ingredient from the database
    /// </summary>
    /// <param name="id">an integer for id</param>
    /// <exception cref="Exception">throws if an ingredient cannot be found</exception>
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
    /// <summary>
    /// This method gets an ingredient from the database based on name
    /// </summary>
    /// <param name="name">takes in a name used to find an ingredient</param>
    /// <returns>a found ingredient</returns>
    public async Task<Ingredient?> GetByNameAsync(string name)
    {
        var found = await context.Ingredients
            .AsNoTracking().SingleOrDefaultAsync(i => i.Name == name);
        return found;
    }
}