using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Model;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class IngredientController : ControllerBase
{
    private readonly IIngredientLogic ingredientLogic;

    public IngredientController(IIngredientLogic ingredientLogic)
    {
        this.ingredientLogic = ingredientLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Ingredient>> CreateAsync(IngredientCreationDto dto)
    {
        try
        {
            Ingredient ingredient = await ingredientLogic.CreateAsync(dto);
            return Created($"/ingredients/{ingredient.Id}", ingredient);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}