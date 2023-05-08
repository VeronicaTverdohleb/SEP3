using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos;
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

    public async Task<MenuBasicDto> GetMenuByDateAsync(DateTime date)
    {
        string query = ConstructQuery(date);
        HttpResponseMessage response = await client.GetAsync("/GetMenu" + query);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        MenuBasicDto menu = JsonSerializer.Deserialize<MenuBasicDto>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return menu;
    }
    
    private static string ConstructQuery(DateTime date)
    {
        return $"?date={date}";
    }
    
    
}