using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Model;

namespace EfcDataAccess.DAOs;

public class MenuDao : IMenuDao
{
    private readonly DataContext context;

    public MenuDao(DataContext context)
    {
        this.context = context;
    }
    
    public async Task<IEnumerable<Item>> GetItemsByDateAsync(DateTime date)
    {
        //return context.Items.Include(item => item.Date);
        return null;
    }
}