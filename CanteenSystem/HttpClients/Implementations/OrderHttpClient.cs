using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace HttpClients.Implementations;

public class OrderHttpClient:IOrderService
{
    private readonly HttpClient client;

    public OrderHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<IEnumerable<Order>> getAllOrdersAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/Order");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Order> orders = JsonSerializer.Deserialize<ICollection<Order>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return orders;
    }

    public async Task<OrderBasicDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/orders/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        OrderBasicDto order = JsonSerializer.Deserialize<OrderBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return order;
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