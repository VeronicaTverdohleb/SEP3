using Shared.Model;

namespace Shared.Dtos.IngredientDto;

public class IngredientCreationDto
{
    public string Name { get; set; }
    public int Amount { get; set; }
    public int Allergen { get; set; }
    
    public IngredientCreationDto(string name, int amount, int allergen)
    {
        Name = name;
        Amount = amount;
        Allergen = allergen;
    }
}