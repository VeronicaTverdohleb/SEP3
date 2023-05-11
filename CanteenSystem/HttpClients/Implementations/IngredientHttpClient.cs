using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Model;

namespace HttpClients.Implementations;

public class IngredientHttpClient:IIngredientService
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
}