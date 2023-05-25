using System.Net.Http.Json;
using System.Text;
using HttpClients.ClientInterfaces;
using Shared.Dtos.IngredientDto;
using Shared.Model;
using System.Text.Json;

namespace HttpClients.Implementations;

public class IngredientHttpClient : IIngredientService
{
    private readonly HttpClient client;

    public IngredientHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
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
    public async Task CreateAsync(IngredientCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Ingredient", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

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
    
    public Task<IngredientBasicDto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    //doesn't work
    public async Task<IngredientBasicDto?> GetByNameAsync(string name)
    {
        HttpResponseMessage response = await client.GetAsync($"/Ingredient/{name}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        IngredientBasicDto ingredient = JsonSerializer.Deserialize<IngredientBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return ingredient;
    }

    public Task<Ingredient?> GetByIdAsyncFromIng(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Ingredient?> GetByNameAsyncFromIng(string name)
    {
        throw new NotImplementedException();
    }

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