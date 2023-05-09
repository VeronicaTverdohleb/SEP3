using System.Text.Json.Serialization;

namespace Shared.Model;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public int Allergen { get; set; }
    public ICollection<Item> Items { get; set; }

    public Ingredient(string name, int amount, int allergens)
    {
        Allergen = allergens;
        Name = name;
        Amount = amount;
    }
    
    private Ingredient() {}

}