using Application.DaoInterfaces;
using Application.LogicInterfaces;

namespace Application.Logic;

public class OrderLogic : IOrderLogic
{
    private readonly IOrderDao orderDao;

    public OrderLogic(IOrderDao orderDao)
    {
        this.orderDao = orderDao;
    }

}