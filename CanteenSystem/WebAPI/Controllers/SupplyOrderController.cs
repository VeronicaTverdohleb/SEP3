using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplyOrderController : ControllerBase
{
    private readonly ISupplyOrderLogic supplyOrderLogic;

    public SupplyOrderController(ISupplyOrderLogic supplyOrderLogic)
    {
        this.supplyOrderLogic = supplyOrderLogic;
    }

    
}