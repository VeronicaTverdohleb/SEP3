using Shared.Model;

namespace Shared.Dtos;

public class OrderFullInfoDto
{
    public int? Id { get; }
    public User? Customer { get; set; }
    public string? Status { get; set; }
    public DateOnly? Date { get; set; }
    public ICollection<Item>? Items { get; set; }
    public ICollection<Ingredient>? Ingredients { get; set; }
}