using Shared.Model;

namespace Shared.Dtos;

public class OrderUpdateDto
{
    public int Id { get; }
    public ICollection<Item> Items { get; set; }
    public string Status { get; set; }

    public OrderUpdateDto(int id, ICollection<Item> items, string status)
    {
        Id = id;
        Items = items;
        Status = status;
    }
}