namespace Shared.Dtos.IngredientDto;

public class IngredientUpdateDto
{ 
    public int Id { get; set; }
    public int Amount { get; set; }
    
    public IngredientUpdateDto(int Id, int amount)
    {
        this.Id = Id;
        Amount = amount;
    }
    public IngredientUpdateDto(){}
}