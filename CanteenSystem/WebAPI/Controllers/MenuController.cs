using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Model;

namespace WebAPI.Controllers;

/// <summary>
/// Web API method definition related to Menu functionality
/// Includes methods - CreateAsync(MenuBasicDto dto), UpdateAsync(MenuUpdateDto dto), GetAsync(DateOnly date)
/// </summary>
[ApiController]
[Route("[controller]")]
public class MenuController: ControllerBase
{
    private readonly IMenuLogic menuLogic;

    public MenuController(IMenuLogic menuLogic)
    {
        this.menuLogic = menuLogic;
    }
    
    /// <summary>
    /// POST endpoint that accepts MenuBasicDto as a parameter and call the MenuLogic
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// GET endpoint that accepts DateOnly as a parameter and calls the MenuLogic
    /// To get Menu by date
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// UPDATE endpoint that accepts MenuUpdateDto as a parameter and calls the MenuLogic
    /// To Update the Menu
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
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