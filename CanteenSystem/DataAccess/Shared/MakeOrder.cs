using Shared.Model;

namespace EfcDataAccess.Shared;

public class MakeOrder
{
    public List<int> ItemIds { get; set; }
    public DateOnly Date { get;}
  
    public User Customer { get; set; }
    public string Status { get; }
    
    public MakeOrder(User customer, DateOnly date, string status,  List<int> itemIds)
    {
        Customer = customer;
        Status = status;
        Date = date;
        ItemIds = itemIds;
    }
}