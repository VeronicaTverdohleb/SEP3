using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos;
using Shared.Dtos.IngredientDto;
using Shared.Dtos.SupplyDto;
using Shared.Model;

namespace HttpClients.Implementations;

public class SupplyOrderHttpClient : ISupplyOrderService
{
    private readonly HttpClient client;

    public SupplyOrderHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task CreateAsync(SupplyOrderCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/supplyOrders", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<SupplyOrder>> GetAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/SupplyOrders");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<SupplyOrder> supplyOrders = JsonSerializer.Deserialize<ICollection<SupplyOrder>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return supplyOrders;
    }
/*
    public async Task<Supplier?> GetSupplierByName(string name)
    {
        HttpResponseMessage response = await client.GetAsync($"/supplier/{name}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Supplier supplier = JsonSerializer.Deserialize<Supplier>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return supplier;
    }
    */
}