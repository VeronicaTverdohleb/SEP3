using Shared.Model;

namespace Shared.Dtos;

public class MakeOrderDto
{
    public List<int> ItemIds { get; set; }
    public DateOnly Date { get;}
  
    public User Customer { get; private set; }
    public string Status { get; }
    
    public MakeOrderDto(User customer, DateOnly date, string status,  List<int> itemIds)
    {
        Customer = customer;
        Status = status;
        Date = date;
        ItemIds = itemIds;
    }
}