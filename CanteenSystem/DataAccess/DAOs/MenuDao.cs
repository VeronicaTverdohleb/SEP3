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
    
    public async Task<Menu> GetMenuByDateAsync(DateTime date)
    {
        String newDate = date.Year + "-" + date.Month + "-" + date.Day;

        return null;
        /***
        Menu? found = await context.Menus
            .AsNoTracking()
            .SingleOrDefaultAsync(menu => menu.Date.Equals(newDate));
        return found;
        ***/
    }
}