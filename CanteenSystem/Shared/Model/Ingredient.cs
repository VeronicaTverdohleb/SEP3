using System.Text.Json.Serialization;

namespace Shared.Model;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public int Allergen { get; set; }
    
     [JsonIgnore]
    public ICollection<Item> Items { get; set; }

    public Ingredient(string name, int amount, int allergen)
    {
        Allergen = allergen;
        Name = name;
        Amount = amount;
        
    }
    
    private Ingredient() {}

}