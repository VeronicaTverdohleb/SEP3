using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    
    public async Task<Menu> CreateAsync(Menu menu)
    {
        EntityEntry<Menu> added = await context.Menus.AddAsync(menu);
        await context.SaveChangesAsync();
        return added.Entity;
    }
    

    public async Task<MenuBasicDto> GetMenuByDateAsync(DateOnly date)
    {
        List<ItemMenuDto> newItems = new List<ItemMenuDto>();
        // We need format yyyy-mm-dd, but the DateTime creates yyyy-m-d when the Month/Date are only one digit

        Menu? foundMenu = context.Menus
            .AsNoTracking()
            .Include(menu => menu.Items)
            .FirstOrDefault(menu => menu.Date == date);
        
        if (foundMenu == null)
            throw new Exception("There is no Menu on this date");

        if (!foundMenu.Items.Any())
            Console.WriteLine("There are no Items on this Menu");

        foreach (Item item in foundMenu.Items)
        {
            String ingredients = "";
            String allergens = "";
            Item? foundItem = context.Items
                .AsNoTracking()
                .Include(item2 => item2.Ingredients)
                .FirstOrDefault(item3 => item3.Id == item.Id);
            if (foundItem == null)
                throw new Exception("There are no Items with this Id");

            if (foundItem.Ingredients == null)
                throw new Exception("There are no Ingredients in this Item");

            foreach (Ingredient ingredient in foundItem.Ingredients)
            {
                if (foundItem.Ingredients.Last().Equals(ingredient))
                {
                    ingredients += ingredient.Name;
                    if (ingredient.Allergen != 0)
                        allergens += ingredient.Allergen;
                }
                else
                {
                    ingredients += ingredient.Name + ", ";
                    allergens += ingredient.Allergen + ", ";
                }
            }

            ItemMenuDto newItem = new ItemMenuDto(item.Id, foundMenu.Id, item.Name, ingredients, allergens);
            newItems.Add(newItem);
        }


        if (newItems == null || !newItems.Any())
            throw new Exception("There are no Items on this Menu");
        MenuBasicDto menu = new MenuBasicDto(newItems, date);
        return menu;
    }


    public async Task UpdateMenuAsync(MenuUpdateDto dto)
    {
        ICollection<Item> items = new List<Item>();

        Item? foundItem = context.Items
            .AsNoTracking()
            .FirstOrDefault(item => item.Id == dto.ItemId);
        if (foundItem != null)
        {
            items.Add(foundItem);
            Console.WriteLine($"Item in the Update body: {foundItem.Id}");
        }

        Menu? foundMenu = context.Menus
            .AsNoTracking()
            .Include(menu => menu.Items)
            .FirstOrDefault(menu => menu.Date == dto.Date);

        Menu menu = new Menu(dto.Date, items)
        {
            Id = foundMenu.Id
        };

        
        if (dto.Action.Equals("remove".ToLower()))
        {
            context.Menus.Remove(foundMenu);
            await context.SaveChangesAsync();
            
            foreach (Item i in foundMenu.Items)
            {
                if (i.Id == dto.ItemId)
                    foundMenu.Items.Remove(i);
            }

            Menu newMenu = new Menu(dto.Date, foundMenu.Items);

            await context.Menus.AddAsync(newMenu);
            await context.SaveChangesAsync();
            
            //context.Menus.Update(menu);
            Console.WriteLine("Removed item");
        }
        else if (dto.Action.Equals("add".ToLower()))
        {
            context.Menus.Update(menu);
        }
        await context.SaveChangesAsync();
    }
    
}