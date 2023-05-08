using System.Text.Json.Serialization;

namespace Shared.Model;

public class Menu
{
    [JsonIgnore]
    public ICollection<Item> Items { get; set; }
    public DateTime Date { get; set; }
    
    private Menu() {}

}