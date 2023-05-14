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
    public Task<IEnumerable<Order>> GetAllOrdersAsync(SearchOrderParametersDto searchParameters)
    {
        IEnumerable<Order> result = context.Orders.Include(order => order.Customer).Include(order=>order.Items).AsEnumerable();

        if (searchParameters.Id!=null)
        {
            result = result.Where(o => o.Id == searchParameters.Id);
        }

        if (searchParameters.Date.HasValue)
        {
            result = result.Where(o => o.Date.ToShortDateString().Equals(searchParameters.Date.ToString()));
        }

        if (!string.IsNullOrEmpty(searchParameters.UserName))
        {
            result = result.Where(o => o.Customer.UserName.Equals(searchParameters.UserName));
        }

        if (!string.IsNullOrEmpty(searchParameters.CompletedStatus))
        {
            result = result.Where(o => o.Status.Equals(searchParameters.CompletedStatus));
        }

        return Task.FromResult(result);
        
        /*//IQueryable<Order> orderQuery = context.Orders.AsQueryable();
        IQueryable<Order> orderQuery = context.Orders.Include(order => order.Customer).Include(order=>order.Items).AsQueryable();
        
        List<Order> result = await orderQuery.ToListAsync();
        return result;*/
    }

    public Task<Order> GetByIdAsync(int id)
    {
        Order? existing = context.Orders.Include(order => order.Customer).Include(order => order.Items).FirstOrDefault(o =>
            o.Id == id
        );
        return Task.FromResult(existing);
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        EntityEntry<Order> added = await context.Orders.AddAsync(order);
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