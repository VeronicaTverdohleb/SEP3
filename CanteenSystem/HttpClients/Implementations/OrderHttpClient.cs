using HttpClients.ClientInterfaces;
using Shared.Dtos;

namespace HttpClients.Implementations;

public class OrderHttpClient:IOrderService
{
    public Task<OrderBasicDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(OrderUpdateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}