using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace Application.Logic;

/// <summary>
/// Logic for Async methods used in Web API
/// </summary>
public class MenuLogic: IMenuLogic
{
    private readonly IMenuDao menuDao;
    private readonly IItemDao itemDao;

    public MenuLogic(IMenuDao menuDao, IItemDao itemDao)
    {
        this.menuDao = menuDao;
        this.itemDao = itemDao;
    }

    public async Task<MenuBasicDto> GetMenuByDateAsync(DateOnly date)
    {
        MenuBasicDto? menu = await menuDao.GetMenuByDateAsync(date);
        if (menu == null)
            throw new Exception($"There is no Menu on this date");
        if (menu.Items == null || !menu.Items.Any())
            throw new Exception("There are no Items on this Menu");
        
        return menu;
    }
    

    public async Task UpdateMenuAsync(MenuUpdateDto dto)
    {
        MenuBasicDto? menu = await menuDao.GetMenuByDateAsync(dto.Date);
        if (menu == null)
        {
            throw new Exception($"There is no Menu on this date");
        }
        
        Item? item = await itemDao.GetByIdAsync(dto.ItemId);
        if (item == null)
        {
            throw new Exception($"You cannot add/remove this Item to the Menu because Item with ID {dto.ItemId} was not found");
        }
        await menuDao.UpdateMenuAsync(dto);
    }

    public async Task<Menu> CreateAsync(MenuBasicDto dto)
    {
        MenuBasicDto? menu = await menuDao.GetMenuByDateAsync(dto.Date);
        if (menu != null)
        {
            throw new Exception($"There is already Menu on this date");
        }

        Menu newMenu = new Menu(dto.Date, new List<Item>());
        Menu created = await menuDao.CreateAsync(newMenu);
        return created;
    }

}