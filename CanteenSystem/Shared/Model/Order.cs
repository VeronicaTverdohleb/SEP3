using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using Shared.Dtos;

namespace Shared.Model;

public class Order
{
    public int Id { get; set; }
    public User Customer { get; set; }    
    public string Status { get; set; }
    public DateOnly Date { get; set; }

    
    public ICollection<Item> Items { get; set; }
    
    public Order(User customer, DateOnly date, string status, ICollection<Item> items)
    {
        Customer = customer;
        Date = date;
        Status = status;
        Items = items;
    }
    
    
    public Order() {}

    public Order(ICollection<Item> items, string status)
    {
        Items = items;
        Status = status;
    }
    
}