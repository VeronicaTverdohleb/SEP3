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

    public async Task<ICollection<Order>> getAllOrdersAsync(string? userName, string? completedStatus)
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

    public async Task<OrderCreationDto> GetOrderByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/orders/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        OrderCreationDto order = JsonSerializer.Deserialize<OrderCreationDto>(content, 
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

    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"Orders/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}