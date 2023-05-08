namespace Shared.Model;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public List<int> Allergens { get; set; }
    
    public Ingredient(string Name, int Amount, List<int> Allergens)
    {
        this.Allergens = Allergens;
        this.Name = Name;
        this.Amount = Amount;
    }
}