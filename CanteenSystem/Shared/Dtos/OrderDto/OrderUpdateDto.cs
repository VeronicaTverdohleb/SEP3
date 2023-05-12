using Shared.Model;

namespace Shared.Dtos;

public class OrderUpdateDto
{
    public int Id { get; }
    public string? CustomerName { get; set; }
    public ICollection<Item>? Items { get; set; }
    public string? Status { get; set; }

    public OrderUpdateDto(int id)
    {
        Id = id;
    }
}