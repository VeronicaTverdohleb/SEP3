using Shared.Model;

namespace Shared.Dtos;

public class MakeOrderDto
{
    public List<int> ItemIds { get;  }
    public DateOnly Date { get;}
  
    public int CustomerId { get;  }
    public string Status { get; }
    
    public MakeOrderDto(int customerId, DateOnly date, string status,  List<int> itemIds)
    {
        CustomerId = customerId;
        Status = status;
        Date = date;
        ItemIds = itemIds;
    }
}