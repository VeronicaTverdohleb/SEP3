namespace Shared.Dtos.IngredientDto;

public class IngredientUpdateDto
{ 
    public int Id { get; }
    public int Amount { get; set; }

    public IngredientUpdateDto(int Id)
    {
        this.Id = Id;
    }
}