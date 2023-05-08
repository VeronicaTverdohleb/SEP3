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
    public async Task<IEnumerable<Order>> GetAllPostsAsync()
    {
        IQueryable<Order> orderQuery = context.Orders.AsQueryable();

        List<Order> result = await orderQuery.ToListAsync();
        return result;
    }
}