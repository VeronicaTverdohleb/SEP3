using Shared.Model;

namespace Application.DaoInterfaces;

public interface IOrderDao
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order> GetByIdAsync(int id);
}