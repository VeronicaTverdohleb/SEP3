using System.Text.Json.Serialization.Metadata;
using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Model;

namespace EfcDataAccess.DAOs;

public class ItemDao : IItemDao
{
    private readonly DataContext context;

    public ItemDao(DataContext context)
    {
        this.context = context;
    }
    public async Task<Item> CreateAsync(ItemCreationDto dto)
    {
        // Name, price
        // Feel free to add the logic part again (not necessary)
        

        ICollection<Ingredient> ingredients = new List<Ingredient>();

        foreach (int IngredientId in dto.IngredientIds)
        {
            Ingredient? ingredient = context.Ingredients.FirstOrDefault(ingredient => ingredient.Id == IngredientId);
            ingredients.Add(ingredient);
        }
        
        Item newItem = new Item(dto.Name, dto.Price, ingredients);
        EntityEntry<Item> added = await context.Items.AddAsync(newItem);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Item>> GetAsync(SearchItemSto searchParameters)
    {
        IQueryable<Item> query = context.Items.Include(item => item.Ingredients).AsQueryable();
        

        if (!string.IsNullOrEmpty(searchParameters.NameContains))
        {
            query = query.Where(item =>
                item.Name.ToLower().Contains(searchParameters.NameContains.ToLower()));
        }

        List<Item> result = await query.ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Item>> GetAllItemsAsync()
    {
        IEnumerable<Item> list = context.Items.ToList();
        return list;
    }

    public async Task UpdateAsync(Item item)
    {
        context.Items.Update(item);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Item? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Item with id {id} not found");

        }
        context.Items.Remove(existing);
        await context.SaveChangesAsync();
    }

    public async Task<Item?> GetByNameAsync(string name)
    {
        Item? found = await context.Items
            .AsNoTracking().Include(item => item.Ingredients)
            .SingleOrDefaultAsync(item => item.Name.Equals(name));
        return found;
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        Item? found = await context.Items
            .AsNoTracking().Include(item => item.Ingredients)
            .SingleOrDefaultAsync(item => item.Id == id);
        return found;
    }
}