using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Model;

namespace Application.Logic;

public class OrderLogic : IOrderLogic
{
    private readonly IOrderDao orderDao;

    public OrderLogic(IOrderDao orderDao)
    {
        this.orderDao = orderDao;
    }

    public Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return orderDao.GetAllOrdersAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        Order? order = await orderDao.GetByIdAsync(id);
        if (order == null)
        {
            throw new Exception($"Order with id {id} not found");
        }

        return order;
    }
    
    public async Task DeleteAsync(int id)
    {
        Order? order = await orderDao.GetByIdAsync(id);
        if (order == null)
        {
            throw new Exception($"Order with ID {id} was not found!");
        }

        await orderDao.DeleteAsync(id);
    }
}