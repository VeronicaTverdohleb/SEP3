using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuController: ControllerBase
{
    private readonly IMenuLogic menuLogic;

    public MenuController(IMenuLogic menuLogic)
    {
        this.menuLogic = menuLogic;
    }
    
    [HttpGet, Route("/GetItems")]
    public async Task<ActionResult<IEnumerable<Item>>> GetAsync([FromQuery] DateTime date)
    {
        try
        {
            IEnumerable<Item> items = await menuLogic.GetItemsByDateAsync(date);
            return Ok(items);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}