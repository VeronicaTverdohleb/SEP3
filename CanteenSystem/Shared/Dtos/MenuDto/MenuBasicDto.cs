using Shared.Model;

namespace Shared.Dtos;

public class MenuBasicDto
{
    public List<ItemMenuDto> Items { get; set; }
    public DateTime Date { get; set; }

    public MenuBasicDto(List<ItemMenuDto> items, DateTime date)
    {
        Items = items;
        Date = date;
    }
    
}