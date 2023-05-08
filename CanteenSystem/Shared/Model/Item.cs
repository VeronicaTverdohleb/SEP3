
namespace Shared.Model;

public class Item
{
    public int Id { get; set; }
    public string name { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    //public List<DateTime> Date { get; set; }
    
    private Item() {}

}