using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Dtos.SupplyDto;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplyOrderController : ControllerBase
{
    private readonly ISupplyOrderLogic supplyOrderLogic;

    public SupplyOrderController(ISupplyOrderLogic supplyOrderLogic)
    {
        this.supplyOrderLogic = supplyOrderLogic;
    }
    
    /*
    [HttpPost]
    public async Task<ActionResult<Supplier>> CreateAsync(SupplyOrderCreationDto dto)
    {
        try
        {
            SupplyOrder supplyOrder = await supplyOrderLogic.CreateAsync(dto);
            return Created($"/supplyOrders/{supplyOrder.Id}", supplyOrder);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetAsync()
    {
        try
        {
            var supplyOrders = await supplyOrderLogic.GetAsync();
            return Ok(supplyOrders);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    */
    
}