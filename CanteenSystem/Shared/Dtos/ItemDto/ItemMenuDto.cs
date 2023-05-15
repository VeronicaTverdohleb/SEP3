namespace Shared.Dtos;

public class ItemMenuDto
{
    public String Name { get; set; }
    public String IngredientNames { get; set; }
    public String Allergens { get; set; }

    public ItemMenuDto(String name, String ingredientNames, String allergens)
    {
        Name = name;
        IngredientNames = ingredientNames;
        Allergens = allergens;
    }
}