using Shared.Model;

namespace Shared.Dtos;

public class ItemBasicDto
{

   
    public string Name { get; set; }
    public double Price { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }

    public ItemBasicDto( string name, double price, ICollection<Ingredient> ingredients)
    {
        Name = name;
        Price = price;
        Ingredients = ingredients;
    }

}