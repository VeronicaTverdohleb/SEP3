using System.Text.Json.Serialization;

namespace Shared.Model;

public class Order
{
    public int Id { get; set; }
    public User Customer { get; set; }    
    public string Status { get; set; }
    [JsonIgnore]
    public ICollection<Item> Items { get; set; }
    public Order() {}
    
}