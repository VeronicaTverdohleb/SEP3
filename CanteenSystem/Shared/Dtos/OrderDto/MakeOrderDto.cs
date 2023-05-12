using Shared.Model;

namespace Shared.Dtos;

public class MakeOrderDto
{
    public List<int> ItemIds { get; set; }
    public DateTime Date { get; }
    public User Customer { get;  }
    public string Status { get; }
    
    public MakeOrderDto(User customer, string status,  List<int> itemIds)
    {
        Customer = customer;
        Status = status;
       // Date = date;
        ItemIds = itemIds;
    }
}