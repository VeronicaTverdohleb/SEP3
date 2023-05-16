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

    public Task<IEnumerable<Order>> GetAllOrdersAsync(SearchOrderParametersDto parameters)
    {
        return orderDao.GetAllOrdersAsync(parameters);
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
       
        Order created = await orderDao.CreateOrderAsync(dto);
        return created;
    }

    public async Task UpdateOrderAsync(OrderUpdateDto dto)
    {
        Order? existing = await orderDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Order with ID {dto.Id} not found!");
        }

        if (dto.Status.Equals("completed"))
        {
            throw new Exception("Cannot un-complete a completed Order");
        }

        User userToUse =  existing.Customer;
        DateOnly dateToUse = existing.Date;

        Order updated = new (dto.Items ,dto.Status)
        {
            Customer = userToUse,
            Date = dateToUse
        };

        ValidateOrder(updated);

        await orderDao.UpdateOrderAsync(updated);
    }
    
    private void ValidateOrder(Order dto)
    {
        if (dto.Items.Count == 0)
        {
            throw new Exception("Order will be empty! delete instead!");
        }
        // other validation stuff
    }
}