using Shared.Model;

namespace Shared.Dtos;

public class OrderUpdateDto
{
    public int Id { get; }
    public string? CustomerName { get; }
    public IEnumerable<Item>? items { get; set; }
    public string? status { get; set; }

    public OrderUpdateDto(int id)
    {
        Id = id;
    }
}