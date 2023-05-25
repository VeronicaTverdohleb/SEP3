using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Model;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemLogic itemLogic;

    /// <summary>
    /// Controller
    /// </summary>
    /// <param name="itemLogic"></param>
    public ItemController(IItemLogic itemLogic)
    {
        this.itemLogic = itemLogic;
    }

    /// <summary>
    /// Create method which returns a new Item
    /// Checks if there are ingredients to create the Item, if an Item with
    /// the same name exists and if the user is trying to create an Item without ingredients
    /// </summary>
    /// <param name="dto">Takes the ItemCreationDto</param>
    /// <returns>Created item</returns>
    [HttpPost]
    public async Task<ActionResult<Item>> CreateAsync(ItemCreationDto dto)
    {
        try
        {
            Item item = await itemLogic.CreateAsync(dto);
            return Created($"/items/{item.Id}", item);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Gets all the existing Items
    /// </summary>
    /// <param name="name">he user can find all Items with this name</param>
    /// <returns>An enumerable list</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetAllItemsAsync([FromQuery] string? name)
    {
        try
        {
            SearchItemSto parameters = new(name,null);
            var items = await itemLogic.GetAllItemsAsync(parameters);
            return Ok(items);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }

    }

    
    /// <summary>
    ///  Delete method which checks if the id of the Item selected exists and if yes it gets removed
    /// </summary>
    /// <param name="id">Id of the selected Item</param>
    /// <returns>A successful/unsuccessful code</returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await itemLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
   /// <summary>
   /// Gets an existing Item by the inputted id
   /// </summary>
   /// <param name="id">Id of the Item</param>
   /// <returns>Item which has the inputted id</returns>
    [HttpGet, Route("/items/{id:int}")]
    public async Task<ActionResult<ItemBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            ItemBasicDto result = (await itemLogic.GetByIdAsync(id))!;
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
   /// <summary>
   /// Gets an existing Item by the inputted name
   /// </summary>
   /// <param name="name">Name of the Item</param>
   /// <returns>Item which has the inputted name</returns>
    [HttpGet, Route("/item/{name}")]
    public async Task<ActionResult<ItemBasicDto>> GetByName([FromQuery] string name)
    {
        try
        {
            ItemBasicDto result = (await itemLogic.GetByNameAsync(name))!;
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    
    
}