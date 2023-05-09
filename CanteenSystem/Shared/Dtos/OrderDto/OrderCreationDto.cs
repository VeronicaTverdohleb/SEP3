using Shared.Model;

namespace Shared.Dtos;

public class OrderCreationDto
{
    public User Customer { get;  }
    public string Status { get; }
    public List<Item> Items { get; }

    public OrderCreationDto(User customer, string status, List<Item> items)
    { 
        Customer = customer;
        Status = status;
        Items = items;
    }
}