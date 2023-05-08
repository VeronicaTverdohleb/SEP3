
namespace Shared.Model;

public class Item
{
    public int Id { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    
    private Item() {}

}