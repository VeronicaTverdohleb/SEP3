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
    
    [HttpPost]
    public async Task<ActionResult<Menu>> CreateAsync([FromBody]MenuBasicDto dto)
    {
        try
        {
            Menu created = await menuLogic.CreateAsync(dto);
            return Created($"/Menu/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet, Route("/Menu")]
    public async Task<ActionResult<Menu>> GetAsync([FromQuery] DateOnly date)
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
    
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] MenuUpdateDto dto)
    {
        try
        {
            await menuLogic.UpdateMenuAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}