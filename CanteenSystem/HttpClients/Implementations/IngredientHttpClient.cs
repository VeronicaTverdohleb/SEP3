using System.Net.Http.Json;
using System.Text;
using HttpClients.ClientInterfaces;
using Shared.Dtos.IngredientDto;
using Shared.Model;
using System.Text.Json;

namespace HttpClients.Implementations;
/// <summary>
/// This class is used to interact with the WebAPI
/// </summary>
public class IngredientHttpClient : IIngredientService
{
    private readonly HttpClient client;
    /// <summary>
    /// This constructor instantiates the HttpClient
    /// </summary>
    /// <param name="client">takes in an HttpClient</param>
    public IngredientHttpClient(HttpClient client)
    {
        this.client = client;
    }
    /// <summary>
    /// This method calls the GetIngredientsAsync
    /// </summary>
    /// <param name="name">can take in a string name</param>
    /// <returns>returns a list of all ingredients in the database</returns>
    /// <exception cref="Exception">throws an exception if the status code in not successful</exception>
    public async Task<ICollection<Ingredient>> getAllIngredientsAsync(string? name)
    {
        HttpResponseMessage response = await client.GetAsync("/Ingredient");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Ingredient> ingredients = JsonSerializer.Deserialize<ICollection<Ingredient>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return ingredients;
    }
    /// <summary>
    /// This method calls the CreateAsync in WebAPI
    /// </summary>
    /// <param name="dto">takes in an IngredientCreationDto</param>
    /// <exception cref="Exception">throws an exception if the status code is not successful</exception>
    public async Task CreateAsync(IngredientCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Ingredient", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    /// <summary>
    /// This method calls the UpdateAsync in WebAPI
    /// </summary>
    /// <param name="dto">takes in an IngredientUpdateDto</param>
    /// <exception cref="Exception">throws an exception if the status code is not successful</exception>
    public async Task UpdateIngredientAmount(IngredientUpdateDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent amount = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/Ingredient", amount);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    /// <summary>
    /// This method calls the DeleteAsync
    /// </summary>
    /// <param name="id">takes in an int Id</param>
    /// <exception cref="Exception">throws an exception if the status code is not successful</exception>
    public async Task DeleteIngredient(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"Ingredient/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}