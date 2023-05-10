using Shared.Model;

namespace Shared.Dtos;

public class SearchItemSto
{
    public string? NameContains { get; }
    public int? IdContains { get; }
    public ICollection<Ingredient?> IngredientsContains { get; }
    
    public SearchItemSto(string? nameContains, int? idContains)
    {
        NameContains = nameContains;
        IdContains = idContains;
        
    }

    
}