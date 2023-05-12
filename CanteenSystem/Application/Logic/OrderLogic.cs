using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace Application.Logic;

public class OrderLogic : IOrderLogic
{
    private readonly IOrderDao orderDao;
    private readonly IItemDao itemDao;
    private readonly IUserDao userDao;

    public OrderLogic(IOrderDao orderDao, IItemDao itemDao, IUserDao userDao)
    {
        this.orderDao = orderDao;
        this.itemDao = itemDao;
        this.userDao = userDao;
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

    public async Task DeleteOrderAsync(int id)
    {
        Order? order = await orderDao.GetByIdAsync(id);
        if (order == null)
        {
            throw new Exception($"Order with ID {id} was not found!");
        }

        await orderDao.DeleteOrderAsync(id);
    }

    public async Task<Order> CreateOrderAsync(MakeOrderDto dto)
    {
        foreach (int item in dto.ItemIds)
        {
            Item? existing = await itemDao.GetByIdAsync(item);
            if (existing == null)
            {
                throw new Exception($"This item you try to use, does not exist!");
            }


        }

        User? user = await userDao.GetByUsernameAsync(dto.Customer.UserName);
        if (user == null)
        {
            throw new Exception($"This user does not exist!");

        }
        Order created = await orderDao.CreateOrderAsync(dto);
        return created;
    }

    public async Task UpdateOrderAsync(OrderUpdateDto order)
    {
        throw new NotImplementedException();
    }
}