using Application.DaoInterfaces;
using Application.LogicInterfaces;

namespace Application.Logic;

public class IngredientLogic : IIngredientLogic
{
    private readonly IIngredientDao ingredientDao;

    public IngredientLogic(IIngredientDao ingredientDao)
    {
        this.ingredientDao = ingredientDao;
    }

}