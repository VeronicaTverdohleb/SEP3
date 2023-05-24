using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos;

namespace HttpClients.Implementations;

/// <summary>
/// HttpClient Class that makes HTTP requests towards Web API
/// </summary>
public class MenuHttpClient : IMenuService
{
    private readonly HttpClient client;
    private IMenuService menuServiceImplementation;

    public MenuHttpClient(HttpClient client)
    {
        this.client = client;
    }

    /// <summary>
    /// HTTP request to Web API to Get Menu by Date
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<MenuBasicDto> GetMenuByDateAsync(DateOnly date)
    {
        string query = ConstructQuery(ConvertDate(date));
        HttpResponseMessage response = await client.GetAsync("/Menu" + query);
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


    /// <summary>
    /// Sends HTTP request to Web API to Update a Menu (either delete or add item)
    /// </summary>
    /// <param name="dto"></param>
    /// <exception cref="Exception"></exception>
    public async Task UpdateMenuAsync(MenuUpdateDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/Menu", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    /// <summary>
    /// HTTP request to create a new Menu
    /// </summary>
    /// <param name="dto"></param>
    /// <exception cref="Exception"></exception>
    public async Task CreateAsync(MenuBasicDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Menu", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    private static string ConstructQuery(String date)
    {
        return $"?date={date}";
    }

    private static string ConvertDate(DateOnly date)
    {
        String newDate;
        if (date.Month < 10 && date.Day < 10)
            newDate = date.Year + "-0" + date.Month + "-0" + date.Day;
        else if (date.Month >= 10 && date.Day < 10)
            newDate = date.Year + "-" + date.Month + "-0" + date.Day;
        else if (date.Month < 10 && date.Day >= 10)
            newDate = date.Year + "-0" + date.Month + "-" + date.Day;
        else
            newDate = date.Year + "-" + date.Month + "-" + date.Day;
        return newDate;
    }
}