using Application.Logic;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Model;

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
    
    [HttpPost]
    public async Task<ActionResult<Item>> CreateAsync(MakeOrderDto dto)
    {
        try
        {
            Order order = await orderLogic.CreateOrderAsync(dto);
            return Created($"/order/{order.Id}", order);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}