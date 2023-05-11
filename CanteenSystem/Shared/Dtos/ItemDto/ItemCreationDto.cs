using Shared.Model;

namespace Shared.Dtos;

public class ItemCreationDto
{
   

    public string Name { get; set; }
    public ICollection<Ingredient> Ingredients { get; }
    public int Id { get;  }
    public int Price { get; set; }
    
    
    public ItemCreationDto(string name, int price, ICollection<Ingredient> Ingredients )
    {
        Name = name;
        Price = price;
        this.Ingredients = Ingredients;
    }
}