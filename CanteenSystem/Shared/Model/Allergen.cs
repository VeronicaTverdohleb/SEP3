using System.Text.Json.Serialization;

namespace Shared.Model;

public class Allergen
{
    public int Code { get; set; }
    
    [JsonIgnore]
    public ICollection<Ingredient> Ingredients { get; set; }
    public Allergen() {}
}