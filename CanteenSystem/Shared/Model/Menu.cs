using System.Text.Json.Serialization;

namespace Shared.Model;

public class Menu
{
    public ICollection<Item> Items { get; set; }
    public DateTime Date { get; set; }
    
    private Menu() {}

    public Menu(ICollection<Item> items, DateTime date)
    {
        Items = items;
        Date = date;
    }

}