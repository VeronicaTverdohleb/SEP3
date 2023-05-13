using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
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
    
    [HttpGet, Route("/GetMenu")]
    public async Task<ActionResult<Menu>> GetAsync([FromQuery] DateTime date)
    {
        try
        {
            MenuBasicDto menu = await menuLogic.GetMenuByDateAsync(date);
            return Ok(menu);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}