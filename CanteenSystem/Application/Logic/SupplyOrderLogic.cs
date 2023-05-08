using Application.DaoInterfaces;
using Application.LogicInterfaces;

namespace Application.Logic;

public class SupplyOrderLogic : ISupplyOrderLogic
{
    private readonly ISupplyOrderDao supplyOrderDao;

    public SupplyOrderLogic(ISupplyOrderDao supplyOrderDao)
    {
        this.supplyOrderDao = supplyOrderDao;
    }
    

}