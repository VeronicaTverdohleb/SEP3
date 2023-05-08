using Shared.Dtos;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IOrderService
{
    Task<OrderBasicDto> GetByIdAsync(int id);
    Task UpdateAsync(OrderUpdateDto dto);
    Task DeleteAsync(int id);

}