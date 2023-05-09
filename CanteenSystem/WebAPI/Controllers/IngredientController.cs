using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Dtos.IngredientDto;
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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetAsync()
    {
        try
        {
            var ingredients = await ingredientLogic.GetAsync();
            return Ok(ingredients);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<IngredientBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            IngredientBasicDto result = (await ingredientLogic.GetByIdAsync(id))!;
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{name:required}")]
    public async Task<ActionResult<IngredientBasicDto>> GetByName([FromQuery] string name)
    {
        try
        {
            IngredientBasicDto result = (await ingredientLogic.GetByNameAsync(name))!;
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] IngredientUpdateDto dto)
    {
        try
        {
            await ingredientLogic.UpdateIngredientAmount(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await ingredientLogic.DeleteIngredient(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}