using Shared.Model;

namespace Shared.Dtos;

public class ItemCreationDto
{
   

    public string name { get; set; }
    public ICollection<Ingredient> Ingredients { get; }
    public int ingredientId { get; set; }
    
    
    public ItemCreationDto(string name, ICollection<Ingredient> Ingredients )
    {
        this.name = name;
        this.Ingredients = Ingredients;
    }
}