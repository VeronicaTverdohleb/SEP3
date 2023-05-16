using Shared.Model;

namespace Shared.Dtos;

public class MenuBasicDto
{
    public List<ItemMenuDto>? Items { get; set; }
    public DateOnly Date { get; set; }

    public MenuBasicDto(List<ItemMenuDto>? items, DateOnly date)
    {
        Items = items;
        Date = date;
    }
    
}