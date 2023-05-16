using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace Application.Logic;

public class MenuLogic: IMenuLogic
{
    private readonly IMenuDao menuDao;

    public MenuLogic(IMenuDao menuDao)
    {
        this.menuDao = menuDao;
    }

    public Task<MenuBasicDto> GetMenuByDateAsync(DateOnly date)
    {
        return menuDao.GetMenuByDateAsync(date);
    }
    

    public Task UpdateMenuAsync(MenuUpdateDto dto)
    {
        return menuDao.UpdateMenuAsync(dto);
    }

    public async Task<Menu> CreateAsync(MenuBasicDto dto)
    {
        Menu menu = new Menu(dto.Date, new List<Item>());
        Menu created = await menuDao.CreateAsync(menu);
        return created;
    }
}