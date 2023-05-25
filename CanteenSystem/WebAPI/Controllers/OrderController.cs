using Application.Logic;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Model;

namespace WebAPI.Controllers;

/// <summary>
/// Class that serves as the controller for the WebApi regarding order related requests
/// </summary>
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderLogic orderLogic;

    public OrderController(IOrderLogic orderLogic)
    {
        this.orderLogic = orderLogic;
    }

    /// <summary>
    /// GET endpoint that accepts any combination of 4 search criteria to get an
    /// Enumerable of orders for
    /// </summary>
    /// <param name="id">Id of the order</param>
    /// <param name="date">Date the order was ordered for</param>
    /// <param name="userName">Username of the user that made the order</param>
    /// <param name="completedStatus">Current status of the order</param>
    /// <returns>Enumerable of order objects corresponding to the selected search criteria</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAsync([FromQuery]int? id, [FromQuery]DateOnly? date, [FromQuery]string? userName, [FromQuery]string? completedStatus)
    {
        try
        {
            SearchOrderParametersDto parameters = new(id, date, userName, completedStatus);
            var posts = await orderLogic.GetAllOrdersAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    /// <summary>
    /// GET endpoint that gets an order by its id
    /// </summary>
    /// <param name="id">Id of the order</param>
    /// <returns>The order that corresponds to the Id</returns>
    [HttpGet, Route("/orders/{id:int}")]
    public async Task<ActionResult<Order>> GetOrderByIdAsync([FromRoute] int id)
    {
        try
        {
            Order order = await orderLogic.GetOrderByIdAsync(id);
            return Ok(order);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// DELETE endpoint that deletes the specified order by its Id
    /// </summary>
    /// <param name="id">Id of the order to delete</param>
    /// <returns></returns>
    [HttpDelete("/Orders/{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await orderLogic.DeleteOrderAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// PATCH endpoint that updates an existing order with new information
    /// </summary>
    /// <param name="dto">Dto with the information needed to update an existing order</param>
    /// <returns></returns>
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] OrderUpdateDto dto)
    {
        try
        {
            await orderLogic.UpdateOrderAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost( "/Order")]
    public async Task<ActionResult<Item>> CreateAsync([FromBody]MakeOrderDto dto)
    {
        try
        {
            Console.WriteLine("in the controller ");
            Order order = await orderLogic.CreateOrderAsync(dto);
            return Created($"/orders/{order.Id}", order);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}