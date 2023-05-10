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
        HttpResponseMessage response = await client.PostAsJsonAsync("/items", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Item>> GetAsync(string? name, int? id, List<Ingredient?> ingredients, string? titleContains)
    {
        string query = ConstructQuery(name, id, ingredients, titleContains);
        HttpResponseMessage response = await client.GetAsync("/items"+query);
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
    
    private static string ConstructQuery(string? name, int? id, List<Ingredient?> ingredients, string? titleContains)
    {
        string query = "";
        if (!string.IsNullOrEmpty(name))
        {
            query += $"?name={name}";
        }

        if (id != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"id={id}";
        }

        if (!string.IsNullOrEmpty(titleContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"titlecontains={titleContains}";
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

    public async Task<ManageItemDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/items/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ManageItemDto itmes = JsonSerializer.Deserialize<ManageItemDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return itmes;
    }

    public async Task<ManageItemDto> GetByNameAsync(string name)
    {
        HttpResponseMessage response = await client.GetAsync($"/items/{name}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ManageItemDto itmes = JsonSerializer.Deserialize<ManageItemDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return itmes;
    }

    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"items/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}