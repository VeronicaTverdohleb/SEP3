using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

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

    
}