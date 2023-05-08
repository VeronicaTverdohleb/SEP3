using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Model;

namespace HttpClients.Implementations;

public class MenuHttpClient : IMenuService
{
    private readonly HttpClient client;
    private IMenuService menuServiceImplementation;

    public MenuHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<IEnumerable<Item>> GetItemsByDateAsync(DateTime? date)
    {
        string query = ConstructQuery(date);
        HttpResponseMessage response = await client.GetAsync("/GetItems" + query);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<Item> items = JsonSerializer.Deserialize<IEnumerable<Item>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return items;
    }
    
    private static string ConstructQuery(DateTime? date)
    {
        string query = "";
        if (date != null)
        {
            query += $"?date={date}";
        }

        return query;
    }
    
    
}