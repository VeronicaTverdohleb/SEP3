using System.Text.Json.Serialization;

namespace Shared.Model;

public class Menu
{
    public int Id { get; set; }
    public ICollection<Item>? Items { get; set; }
    public DateOnly? Date { get; set; }
    
    private Menu() {}

    public Menu(DateOnly date, ICollection<Item>? items)
    {
        Items = items;
        Date = date;
    }
}