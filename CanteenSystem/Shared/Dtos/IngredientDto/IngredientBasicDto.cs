using Shared.Model;

namespace Shared.Dtos.IngredientDto;

public class IngredientBasicDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public int Allergen { get; set; }

    public IngredientBasicDto(int id,string name, int amount, int allergens)
    {
        Id = id;
        Allergen = allergens;
        Name = name;
        Amount = amount;
    }
}