using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Model;

namespace EfcDataAccess.DAOs;

public class OrderDao : IOrderDao
{
    private readonly DataContext context;

    public OrderDao(DataContext context)
    {
        this.context = context;
    }
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        //IQueryable<Order> orderQuery = context.Orders.AsQueryable();
        IQueryable<Order> orderQuery = context.Orders.Include(order => order.Customer).Include(order=>order.Items).AsQueryable();
        
        List<Order> result = await orderQuery.ToListAsync();
        return result;
    }

    public Task<Order> GetByIdAsync(int id)
    {
        Order? existing = context.Orders.Include(order => order.Customer).Include(order => order.Items).FirstOrDefault(o =>
            o.Id == id
        );
        return Task.FromResult(existing);
    }

    public async Task<Order> CreateOrderAsync(MakeOrderDto dto)
    {
        ICollection<Item> items = new List<Item>();
        foreach (int item in dto.ItemIds)
        {
            Item? itemI = context.Items.FirstOrDefault(i => i.Id == item);
            items.Add(itemI);
        }
        
        Order newOrder = new Order(dto.Customer,dto.Status, items);
        EntityEntry<Order> added = await context.Orders.AddAsync(newOrder);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task UpdateOrderAsync(Order order)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int id)
    {
        Order? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Order with id {id} not found");
        }

        context.Orders.Remove(existing);
        await context.SaveChangesAsync();
    }
}