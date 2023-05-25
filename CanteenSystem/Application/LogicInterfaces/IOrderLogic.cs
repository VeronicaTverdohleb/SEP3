using Shared.Dtos;
using Shared.Model;

namespace Application.LogicInterfaces;

/// <summary>
/// Interface implemented by OrderLogic
/// </summary>
public interface IOrderLogic
{
    Task<IEnumerable<Order>> GetAllOrdersAsync(SearchOrderParametersDto searchParameters);
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> CreateOrderAsync(MakeOrderDto dto);
    Task UpdateOrderAsync(OrderUpdateDto order);
    Task DeleteOrderAsync(int id);

    
}

