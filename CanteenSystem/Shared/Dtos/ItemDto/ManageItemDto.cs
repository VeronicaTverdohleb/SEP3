using Shared.Model;

namespace Shared.Dtos;

public class ManageItemDto
{
   

    public string name { get; set; }
    public int Id { get; }
    public int ingredientId { get; set; }
    public ICollection<Ingredient> Ingredients { get; }
    
    public ManageItemDto(string name, int id, int ingredientId)
    {
        this.name = name;
        Id = id;
        this.ingredientId = ingredientId;
    }
    
   
}