using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace HttpClients.Implementations;
/// <summary>
/// Class that makes HTTP requests towards the web API
/// </summary>
public class OrderHttpClient:IOrderService
{
    private readonly HttpClient client;

    public OrderHttpClient(HttpClient client)
    {
        this.client = client;
    }

    /// <summary>
    /// HTTP request to get all orders depending on search criteria
    /// </summary>
    /// <param name="id">unique id of an order</param>
    /// <param name="date">Date the order is ordered for</param>
    /// <param name="userName">Username of the user that made the order</param>
    /// <param name="completedStatus">The status of the order</param>
    /// <returns>A Collection of order that correspond to the chosen search criteria</returns>
    /// <exception cref="Exception"></exception>
    public async Task<ICollection<Order>> getAllOrdersAsync(int? id, DateOnly? date, string? userName, string? completedStatus)
    {
        string query = ConstructQuery(id, date, userName, completedStatus);
        HttpResponseMessage response = await client.GetAsync("/Order"+query);
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

    /// <summary>
    /// Method that constructs the query that will be used to create the requestUri
    /// </summary>
    /// <param name="id">Id of an order</param>
    /// <param name="date">Date that the order is ordered for</param>
    /// <param name="userName">Username of the user that made the order</param>
    /// <param name="completedStatus">Status of the order</param>
    /// <returns>A string that will be part of the requestUri</returns>
    private static string ConstructQuery(int? id, DateOnly? date, string? userName, string? completedStatus)
    {
        string query = "";
        if (id!=null)
        {
            query += $"?id={id}";
        }

        if (date.HasValue)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"date={date.Value.Year+"-"+date.Value.Month+"-"+date.Value.Day}";
        }

        if (!string.IsNullOrEmpty(userName))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"userName={userName}";
        }

        if (!string.IsNullOrEmpty(completedStatus))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"completedStatus={completedStatus}";
        }
        Console.WriteLine(query);
        return query;
    }

    /// <summary>
    /// Method that sends an HTTP request to get an order by its unique id
    /// </summary>
    /// <param name="id">the id of the order</param>
    /// <returns>a dto which holds all information on an order</returns>
    /// <exception cref="Exception"></exception>
    public async Task<OrderFullInfoDto> GetOrderByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/orders/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        OrderFullInfoDto order = JsonSerializer.Deserialize<OrderFullInfoDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return order;
    }

    /// <summary>
    /// Method that sends an HTTP request to update an existing 
    /// </summary>
    /// <param name="dto">A dto holding the necessary information to update an order</param>
    /// <exception cref="Exception"></exception>
    public async Task UpdateAsync(OrderUpdateDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        Console.WriteLine(dtoAsJson);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/order", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        
    }

   
    /// <summary>
    /// Method that sends an HTTP request to delete an order by its unique id
    /// </summary>
    /// <param name="id">The id of the order to delete</param>
    /// <exception cref="Exception"></exception>
    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"Orders/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    /// <summary>
    /// Method that sends an HTTP request to create a new order
    /// </summary>
    /// <param name="dto">Dto holding the information needed to create an order</param>
    /// <exception cref="Exception"></exception>
    public async Task CreateAsync(MakeOrderDto dto)
    {
       
        HttpResponseMessage response = await client.PostAsJsonAsync("/Order", dto);
        if (!response.IsSuccessStatusCode)
        {
            string responsecontent = await response.Content.ReadAsStringAsync();
            throw new Exception(responsecontent);
        }
        
    }
}