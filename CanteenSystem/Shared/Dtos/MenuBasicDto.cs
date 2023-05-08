using Shared.Model;

namespace Shared.Dtos;

public class MenuBasicDto
{
    public List<Item> Items { get; set; }
    public DateTime Date { get; set; }
    
}