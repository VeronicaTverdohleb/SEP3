using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace HttpClients.Implementations;

public class ItemHttpClient :IItemService
{
    private readonly HttpClient client;
    
    public ItemHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task CreateAsync(ItemCreationDto dto)
    {
        String postAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(postAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("/items", content);
        string responsecontent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responsecontent);
        }
    }

    public async Task<ICollection<Item>> GetAsync(string? name)
    {
        
        HttpResponseMessage response = await client.GetAsync("/Item");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Item> items = JsonSerializer.Deserialize<ICollection<Item>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return items;
    }
    
    private static string ConstructQuery(string? name, int? id, int? price)
    {
        string query = "";
        if (!string.IsNullOrEmpty(name))
        {
            query += $"?Name={name}";
        }

        if (id != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"Id={id}";
        }
        if (price!= null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"Price={price}";
        }

        if (!string.IsNullOrEmpty(name))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"namecontains={name}";
        }
        

        return query;
    }

    public async Task UpdateAsync(ManageItemDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/items", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ItemBasicDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/items/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ItemBasicDto items = JsonSerializer.Deserialize<ItemBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return items;
    }

    public async Task<ItemBasicDto> GetByNameAsync(string name)
    {
        HttpResponseMessage response = await client.GetAsync($"/item/{name}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ItemBasicDto items = JsonSerializer.Deserialize<ItemBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return items;
    }

    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"item/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}