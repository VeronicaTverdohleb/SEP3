using Shared.Model;

namespace Shared.Dtos;

public class OrderBasicDto
{
    public int Id { get; }
    public string CustomerName { get; }
    public IEnumerable<Item> Items { get; }

    public OrderBasicDto(int id, string customerName, IEnumerable<Item> items)
    {
        Id = id;
        CustomerName = customerName;
        Items = items;
    }
}