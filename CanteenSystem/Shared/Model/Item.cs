using System.Text.Json.Serialization;

namespace Shared.Model;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    
    public ICollection<Ingredient>? Ingredients { get; set; }

    [JsonIgnore]
    public ICollection<Order>? Orders { get; }
    [JsonIgnore]
    public ICollection<Menu>? Menus { get; }

    //public List<DateTime> Date { get; set; }

    public Item() {}

    public Item(string name, double price, ICollection<Ingredient>? ingredients)
    {
        Name = name;
        Price = price;
        Ingredients = ingredients;
    }

}