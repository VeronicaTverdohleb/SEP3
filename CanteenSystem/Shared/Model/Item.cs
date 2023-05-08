namespace Shared.Model;

public class Item
{
    public int Id { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public string name;
    public int ingredientId { get; set; }
    
    
    public Item(string name, List<Ingredient> ingredients)
    {
        this.name = name;
        Ingredients = ingredients;
    }

    
    //public List<DateTime> Date { get; set; }
    
    private Item() {}

}