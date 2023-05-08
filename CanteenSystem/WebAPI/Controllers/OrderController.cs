using Application.Logic;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<IEnumerable<Order>>> GetAsync()
    {
        try
        {

            var posts = await orderLogic.GetAllPostsAsync();
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}