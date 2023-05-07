using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderLogic orderLogic;

    public OrderController(IOrderLogic orderLogic)
    {
        this.orderLogic = orderLogic;
    }

    
}