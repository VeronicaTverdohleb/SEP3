using Shared.Model;

namespace Shared.Dtos;

public class ItemBasicDto
{

    public int Id { get; }
    public string name { get; set; }
    public ICollection<Ingredient> Ingredients { get; }
    
    public ItemBasicDto(int id, string name, ICollection<Ingredient> ingredients)
    {
        Id = id;
        this.name = name;
        Ingredients = ingredients;
    }

}