using Shared.Model;

namespace Shared.Dtos;

public class ItemCreationDto
{
   

    public string Name { get; set; }

    public List<int> IngredientIds { get; set; }
    public int Id { get;  }
    public double Price { get; set; }
    
    
    public ItemCreationDto(string name, double price, List<int> ingredientIds)
    {
        Name = name;
        Price = price;
        IngredientIds = ingredientIds;
    }
}