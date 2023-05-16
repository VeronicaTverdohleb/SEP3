using Shared.Model;

namespace Shared.Dtos;

public class ManageItemDto
{
   

    public string Name { get; set; }
    public int Id { get; }
    public List<int> ingredientId { get; set; }
    public int Price { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }
    
    public ManageItemDto(string name, int id, ICollection<Ingredient> ingredients , int price)
    {
        Name = name;
        Id = id;
        Price = price;
        Ingredients = ingredients;
    }

    public ManageItemDto(int Id)
    {
        this.Id = Id;
    }
    
   
}