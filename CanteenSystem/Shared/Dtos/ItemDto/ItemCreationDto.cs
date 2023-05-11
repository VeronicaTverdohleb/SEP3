using Shared.Model;

namespace Shared.Dtos;

public class ItemCreationDto
{
   

    public string Name { get; set; }

    public List<int> IngredientIds { get; set; }
    public int Id { get;  }
    public int Price { get; set; }
    
    
    public ItemCreationDto(string name, int price, List<int> ingredientIds)
    {
        Name = name;
        Price = price;
        IngredientIds = ingredientIds;
    }
}