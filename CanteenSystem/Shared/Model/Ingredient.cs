using System.Text.Json.Serialization;

namespace Shared.Model;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    [JsonIgnore]
    public ICollection<Allergen> Allergens { get; set; }
    [JsonIgnore]
    public ICollection<Item> Items { get; set; }

    public Ingredient(string name, int amount, ICollection<Allergen> allergens)
    {
        Allergens = allergens;
        Name = name;
        Amount = amount;
    }
    
    private Ingredient() {}

}