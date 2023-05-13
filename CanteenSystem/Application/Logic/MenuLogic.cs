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

    public Task<MenuBasicDto> GetMenuByDateAsync(DateTime date)
    {
        return menuDao.GetMenuByDateAsync(date);
    }
}