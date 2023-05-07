using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemLogic itemLogic;

    public ItemController(IItemLogic itemLogic)
    {
        this.itemLogic = itemLogic;
    }

    
}