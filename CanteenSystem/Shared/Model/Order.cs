namespace Shared.Model;

public class Order
{
    public int Id { get; set; }
    public List<Item> Items { get; set; }
    public string CustomerName { get; set; }
    public string Status { get; set; }
    
    private Order() {}
    
}