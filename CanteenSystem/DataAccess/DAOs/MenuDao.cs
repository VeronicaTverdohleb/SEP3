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
        // We need format yyyy-mm-dd, but the DateTime creates yyyy-m-d when the Month/Date are only one digit
        String newDate = ConvertDate(date);
        

        IQueryable<Item>? foundItems = context.Items
            .FromSql($"Select * from Items where MenuDate = {newDate}")
            .Include(item => item.Ingredients).AsQueryable();

        if (!foundItems.Any())
            Console.WriteLine($"There are no Items on this date");

        ICollection<Item> items = foundItems.ToList();
        Menu menu = new Menu(items, date);

        return menu;
    }

    private static string ConvertDate(DateTime date)
    {
        String newDate;
        if (date.Month < 10 && date.Day < 10)
            newDate = date.Year + "-0" + date.Month + "-0" + date.Day;
        else if (date.Month >= 10 && date.Day < 10)
            newDate = date.Year + "-" + date.Month + "-0" + date.Day;
        else if (date.Month < 10 && date.Day >= 10)
            newDate = date.Year + "-0" + date.Month + "-" + date.Day;
        else
            newDate = date.Year + "-" + date.Month + "-" + date.Day;
        return newDate;
    }
}