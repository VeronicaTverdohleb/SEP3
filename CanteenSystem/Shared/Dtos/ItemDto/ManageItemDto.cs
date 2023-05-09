using Shared.Model;

namespace Shared.Dtos;

public class ManageItemDto
{
   

    public string name { get; }
    public int Id { get; }
    public int ingredientId { get; set; }
    public ICollection<Ingredient> Ingredients { get; }
    public string? TitleContains { get;}
    
    public ManageItemDto(string name, int id, ICollection<Ingredient> ingredients)
    {
        this.name = name;
        Id = id;
        Ingredients = ingredients;

    }
    
    public ManageItemDto(string name, int id, string? titleContains)
    {
        this.name = name;
        Id = id;
        TitleContains = titleContains;
    }

    public ManageItemDto(string name, int id, int ingredientId)
    {
        this.name = name;
        Id = id;
        this.ingredientId = ingredientId;
    }
    
   
}