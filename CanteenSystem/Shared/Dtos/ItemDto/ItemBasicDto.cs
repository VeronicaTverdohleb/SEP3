using Shared.Model;

namespace Shared.Dtos;

public class ItemBasicDto
{

   
    public string name { get; set; }
    public int Price { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }

    public ItemBasicDto( string name, int price, ICollection<Ingredient> ingredients)
    {
        this.name = name;
        Price = price;
        Ingredients = ingredients;
    }

}