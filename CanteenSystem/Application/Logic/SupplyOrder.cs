using Application.DaoInterfaces;
using Application.LogicInterfaces;

namespace Application.Logic;

public class SupplyOrder : ISupplyOrderLogic
{
    private readonly ISupplyOrderDao supplyOrderDao;

    public SupplyOrder(ISupplyOrderDao supplyOrderDao)
    {
        this.supplyOrderDao = supplyOrderDao;
    }
    

}