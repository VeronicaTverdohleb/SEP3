using Application.DaoInterfaces;
using Application.LogicInterfaces;

namespace Application.Logic;

public class ItemLogic : IItemLogic
{
    private readonly IItemDao itemDao;

    public ItemLogic(IItemDao itemDao)
    {
        this.itemDao = itemDao;
    }

}