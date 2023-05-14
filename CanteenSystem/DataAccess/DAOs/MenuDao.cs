using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Model;

namespace EfcDataAccess.DAOs;

public class MenuDao : IMenuDao
{
    private readonly DataContext context;

    public MenuDao(DataContext context)
    {
        this.context = context;
    }
    
    public async Task<MenuBasicDto> GetMenuByDateAsync(DateTime date)
    {
        List<ItemMenuDto> newItems = new List<ItemMenuDto>();
        // We need format yyyy-mm-dd, but the DateTime creates yyyy-m-d when the Month/Date are only one digit
        String newDate = ConvertDate(date);
        

        IQueryable<Item>? foundItems = context.Items
            .FromSql($"Select * from Items where MenuDate = {newDate}")
            .Include(item => item.Ingredients).AsQueryable();

        if (!foundItems.Any())
            Console.WriteLine($"There are no Items on this date");

        foreach (Item item in foundItems)
        {
            String ingredients = "";
            String allergens = ""; 
            foreach (Ingredient ingredient in item.Ingredients) {
                    if (item.Ingredients.Last().Equals(ingredient)) {
                        ingredients += ingredient.Name;
                        if (ingredient.Allergen != 0)
                            allergens += ingredient.Allergen;
                    }
                    else {
                        ingredients += ingredient.Name + ", ";
                        allergens += ingredient.Allergen + ", ";
                    }
            }

            ItemMenuDto newItem = new ItemMenuDto(item.Name, ingredients, allergens);
            newItems.Add(newItem);
        }

        MenuBasicDto menu = new MenuBasicDto(newItems, date);
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