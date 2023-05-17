namespace Shared.Dtos;

public class MenuUpdateDto
{
    public DateOnly Date { get; set; }
    public int ItemId { get; set; }
    
    public string Action { get; set; }

    public MenuUpdateDto(DateOnly date, int itemId, string action)
    {
        Date = date;
        ItemId = itemId;
        Action = action;
    }
}