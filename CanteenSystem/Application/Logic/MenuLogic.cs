using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Model;

namespace Application.Logic;

public class MenuLogic: IMenuLogic
{
    private readonly IMenuDao menuDao;

    public MenuLogic(IMenuDao menuDao)
    {
        this.menuDao = menuDao;
    }

    public Task<IEnumerable<Item>> GetItemsByDateAsync(DateTime date)
    {
        return menuDao.GetItemsByDateAsync(date);
    }
}