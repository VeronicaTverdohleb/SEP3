using Shared.Model;

namespace Shared.Dtos.IngredientDto;

public class IngredientBasicDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public ICollection<Allergen> Allergens { get; set; }

    public IngredientBasicDto(int id,string name, int amount, ICollection<Allergen> allergens)
    {
        Allergens = allergens;
        Name = name;
        Amount = amount;
    }
}