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
    public async Task<Item> CreateAsync(Item item)
    {
        EntityEntry<Item> added = await context.Items.AddAsync(item);
        await context.SaveChangesAsync();
        return added.Entity;

    }

    public async Task<IEnumerable<Item>> GetAsync(ManageItemDto searchParameters)
    {
        IQueryable<Item> query = context.Items.Include(item => item.Id).AsQueryable();
        

        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            query = query.Where(item =>
                item.name.ToLower().Contains(searchParameters.TitleContains.ToLower()));
        }

        List<Item> result = await query.ToListAsync();
        return result;
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
            .SingleOrDefaultAsync(item => item.name == name);
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