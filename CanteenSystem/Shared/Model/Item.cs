using System.Text.Json.Serialization;

namespace Shared.Model;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    
    public ICollection<Ingredient> Ingredients { get; }

   

    
    //public List<DateTime> Date { get; set; }

    public Item() {}

}