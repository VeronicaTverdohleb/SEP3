using Shared.Dtos;
using Shared.Model;

namespace Application.DaoInterfaces;

public interface IOrderDao
{
    Task<IEnumerable<Order>> GetAllOrdersAsync(SearchOrderParametersDto searchParameters);
    Task<Order> GetByIdAsync(int id);
    Task<Order> CreateOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int id);
}