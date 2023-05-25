using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace Application.Logic;

/// <summary>
/// Logic for async methods used in the web api
/// </summary>
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

    /// <summary>
    /// Gets a IEnumerable of orders corresponding to the chosen search parameters
    /// </summary>
    /// <param name="parameters">Dto containing the username, date, order status and order number to filter by</param>
    /// <returns>IEnumerable of orders corresponding to the chosen search parameters</returns>
    public Task<IEnumerable<Order>> GetAllOrdersAsync(SearchOrderParametersDto parameters)
    {
        return orderDao.GetAllOrdersAsync(parameters);
    }

    /// <summary>
    /// gets a specific order by its unique id
    /// </summary>
    /// <param name="id">id of the order</param>
    /// <returns>The specified order</returns>
    /// <exception cref="Exception">exception if no order is found with the chosen id</exception>
    public async Task<Order> GetOrderByIdAsync(int id)
    {
        Order? order = await orderDao.GetByIdAsync(id);
        if (order == null)
        {
            throw new Exception($"Order with id {id} not found");
        }

        return order;
    }

    /// <summary>
    /// Deletes an order chosen by its id
    /// </summary>
    /// <param name="id">the id of the order that is to be deleted</param>
    /// <exception cref="Exception">exception if no order with the chosen id is found</exception>
    public async Task DeleteOrderAsync(int id)
    {
        Order? order = await orderDao.GetByIdAsync(id);
        if (order == null)
        {
            throw new Exception($"Order with ID {id} was not found!");
        }

        await orderDao.DeleteOrderAsync(id);
    }
    
    /// <summary>
    /// Creates an order
    /// </summary>
    /// <param name="dto">dto containing the necessary information to create an order</param>
    /// <returns>The created order</returns>
    /// <exception cref="Exception">Exceptions for when items that don't exist are part of the dto or if the dto has no items</exception>
    public async Task<Order> CreateOrderAsync(MakeOrderDto dto)
    {
        foreach (int item in dto.ItemIds)
        {
            Item? existing = await itemDao.GetByIdAsync(item);
            if (existing is { Name: null })
            {
                throw new Exception($"This item you try to use, does not exist!");
            }
        }
        if (dto.ItemIds == null || !dto.ItemIds.Any())
        {
            throw new Exception("An order needs to have items");
        }
        Order created = await orderDao.CreateOrderAsync(dto);
        Console.WriteLine(created);
        return created;
        
    }

    /// <summary>
    /// Updates an existing order with new information
    /// </summary>
    /// <param name="dto">dto holding the information that can be changed</param>
    /// <exception cref="Exception">exceptions for when the specified order does not exist,
    /// when changes are attempted to be made on an order that is "ready for pickup"
    /// or to prevent updating the order with an empty list of items</exception>
    public async Task UpdateOrderAsync(OrderUpdateDto dto)
    {
        Order? existing = await orderDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Order with ID {dto.Id} not found!");
        }
        if (existing.Status.Equals("ready for pickup"))
        {
            throw new Exception($"Cannot change status of an order that is ready for pickup!");
        }
        if (dto.Items.FirstOrDefault() == null)
        {
            throw new Exception("Order will be empty! delete instead!");
        }

        Order updated = new (dto.Items,dto.Status)
        {
            Id = existing.Id,
            Customer = existing.Customer,
            Date = existing.Date
        };

        await orderDao.UpdateOrderAsync(updated);
    }
}