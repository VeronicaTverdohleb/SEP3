using Shared.Model;
using SupplyOrder = Application.Logic.SupplyOrder;

namespace EfcDataAccess;

public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Item> Items { get; set; }
    public ICollection<SupplyOrder> SupplyOrders { get; set; }
    
}