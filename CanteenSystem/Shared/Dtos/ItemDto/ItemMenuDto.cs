namespace Shared.Dtos;

public class ItemMenuDto
{
    public int Id { get; set; }
    public int MenuId { get; set; }
    public String Name { get; set; }
    public String IngredientNames { get; set; }
    public String Allergens { get; set; }

    public ItemMenuDto(int id, int menuId, String name, String ingredientNames, String allergens)
    {
        Id = id;
        MenuId = menuId;
        Name = name;
        IngredientNames = ingredientNames;
        Allergens = allergens;
    }
}