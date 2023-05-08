using Shared.Model;

namespace Shared.Dtos;

public class OrderUpdateDto
{
    public int Id { get; }
    public int CustomerName { get; }
    public IEnumerable<Item> items { get; set; }
    public string status { get; set; }
}