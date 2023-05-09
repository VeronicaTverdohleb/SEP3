using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
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
}