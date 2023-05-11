using Shared.Model;

namespace Shared.Dtos;

public class ManageItemDto
{
   

    public string name { get; set; }
    public int Id { get; }
    public int ingredientId { get; set; }
    public int Price { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }
    
    public ManageItemDto(string name, int id, int ingredientId, int price)
    {
        this.name = name;
        Id = id;
        Price = price;
        this.ingredientId = ingredientId;
    }
    
   
}