namespace Shared.Model;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public List<Allergen> Allergens { get; set; }

    public Ingredient(string name, int amount, List<Allergen> allergens)
    {
        Allergens = allergens;
        Name = name;
        Amount = amount;
    }
    
    private Ingredient() {}

}