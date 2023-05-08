using Shared.Model;

namespace Shared.Dtos;

public class IngredientBasicDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public List<Allergen> Allergens { get; set; }

    public IngredientBasicDto(int id,string name, int amount, List<Allergen> allergens)
    {
        Allergens = allergens;
        Name = name;
        Amount = amount;
    }
}