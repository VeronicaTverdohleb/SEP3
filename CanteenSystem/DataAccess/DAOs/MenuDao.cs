using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Model;

namespace EfcDataAccess.DAOs;

/// <summary>
/// This class accesses the Database Context and adds, modifies, deletes data in the database
/// </summary>
public class MenuDao : IMenuDao
{
    private readonly DataContext context;

    public MenuDao(DataContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Method that adds a new Menu to the Database 
    /// </summary>
    /// <param name="menu"></param>
    /// <returns></returns>
    public async Task<Menu> CreateAsync(Menu menu)
    {
        EntityEntry<Menu> added = await context.Menus.AddAsync(menu);
        await context.SaveChangesAsync();
        return added.Entity;
    }
    
    /// <summary>
    /// Method that retrieves a Menu and its items by Date
    /// It turns its Items into ItemMenuDto which includes a single String of Ingredients and Allergens
    /// This was done to avoid using Item Ids and run into a tracking problem with Change Tracker
    /// Also because there is no adjustments done to the Items in Menu, they are only displayed
    /// Therefor only the Name field is needed
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public async Task<MenuBasicDto?> GetMenuByDateAsync(DateOnly date)
    {
        List<ItemMenuDto> newItems = new List<ItemMenuDto>();
        MenuBasicDto menu = new MenuBasicDto(new List<ItemMenuDto>(), date);

        Menu? foundMenu = context.Menus
            .AsNoTracking()
            .Include(menu => menu.Items)
            .FirstOrDefault(menu => menu.Date == date);

        if (foundMenu != null)
        {
            if (foundMenu.Items != null && foundMenu.Items.Any())
            {
                foreach (Item item in foundMenu.Items)
                {
                    String ingredients = "";
                    String allergens = "";
                    Item? foundItem = context.Items
                        .AsNoTracking()
                        .Include(item2 => item2.Ingredients)
                        .FirstOrDefault(item3 => item3.Id == item.Id);
                    if (foundItem?.Ingredients != null)
                    {
                        foreach (Ingredient ingredient in foundItem.Ingredients)
                        {
                            if (foundItem.Ingredients.Last().Equals(ingredient))
                            {
                                ingredients += ingredient.Name;
                                Console.WriteLine($"Allergen: {ingredient.Allergen}");
                                if (ingredient.Allergen is not 0)
                                    allergens += ingredient.Allergen;
                            }
                            else
                            {
                                ingredients += ingredient.Name + ", ";
                                Console.WriteLine($"Allergen: {ingredient.Allergen}");
                                if (ingredient.Allergen is not 0)
                                    allergens += ingredient.Allergen + ", ";
                            }
                        }
                    }

                    ItemMenuDto newItem = new ItemMenuDto(item.Id, foundMenu.Id, item.Name, ingredients, allergens);
                    newItems.Add(newItem);
                }
            }
            
            if (newItems.Any())
            {
                menu = new MenuBasicDto(newItems, date);
            }
            return menu;
        }

        return null;
    }


    /// <summary>
    /// This method Update the Menu based on the Action
    /// Action "remove" - It removes the whole object from the DataContext and adds it without the deleted Item
    /// Action "add" - it updates the DataContext by using a Menu object with MenuId and the Item that is to be added
    /// </summary>
    /// <param name="dto"></param>
    public async Task UpdateMenuAsync(MenuUpdateDto dto)
    {
        ICollection<Item> items = new List<Item>();

        Item? foundItem = context.Items
            .AsNoTracking()
            .FirstOrDefault(item => item.Id == dto.ItemId);
        if (foundItem != null)
        {
            items.Add(foundItem);
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
        }
        else if (dto.Action.Equals("add".ToLower()))
        {
            context.Menus.Update(menu);
        }

        await context.SaveChangesAsync();
    }
}