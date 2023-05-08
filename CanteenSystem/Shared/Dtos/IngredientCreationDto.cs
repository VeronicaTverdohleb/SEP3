namespace Shared.Dtos;

public class IngredientCreationDto
{
    public string Name { get; set; }
    public int Amount { get; set; }
    public List<int> Allergens { get; set; }
}