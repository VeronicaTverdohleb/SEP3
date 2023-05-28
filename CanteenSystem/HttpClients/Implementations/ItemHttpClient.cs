using System.Text;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace HttpClients.Implementations;

public class ItemHttpClient :IItemService
{
    
    private readonly HttpClient client;
    
    /// <summary>
    /// Constructor for ItemHttpClient
    /// </summary>
    /// <param name="client"></param>
    public ItemHttpClient(HttpClient client)
    {
        this.client = client;
    }
  
    /// <summary>
    /// Create method which returns a new Item
    /// Checks if there are ingredients to create the Item, if an Item with
    /// the same name exists and if the user is trying to create an Item without ingredients
    /// </summary> 
    /// <param name="dto">Takes the ItemCreationDto</param>
    /// <returns>a new Item</returns>
    /// <exception cref="Exception"></exception>
    public async Task CreateAsync(ItemCreationDto dto)
    {
        String postAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(postAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("/Item", content);
        string responsecontent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responsecontent);
        }
    }
    /// <summary>
    /// Gets all the existing Items
    /// </summary>
    /// <param name="name">The user can find all Items with this parameter</param>
    /// <returns>All Items which correspond to the parameter</returns>
    public async Task<ICollection<Item>> GetAsync(string? name)
    {
        string query = ConstructQuery(name);

        HttpResponseMessage response = await client.GetAsync("/item"+query);
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
    
    private static string ConstructQuery(string? name)
    {
        string query = "";
        if (!string.IsNullOrEmpty(name))
        {
            query += $"?Name={name}";
        }

       
        if (!string.IsNullOrEmpty(name))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"namecontains={name}";
        }
        

        return query;
    }
    
    /// <summary>
    /// Gets an existing Item by the inputted id
    /// </summary>
    /// <param name="id">Id of the Item</param>
    /// <returns>Item which has the inputted id</returns>
    /// <exception cref="Exception">Appears when no Item with the given id exists</exception>
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

    
    /// <summary>
    /// Delete method which checks if the id of the Item selected exists and if yes it gets removed
    /// </summary>
    /// <param name="id">Id of the selected Item</param>
    /// <exception cref="Exception">Appears when no Item has the selected id</exception>
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