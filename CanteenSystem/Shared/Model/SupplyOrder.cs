namespace Shared.Model;

public class SupplyOrder
{
    public int Id { get; set; }
    public Ingredient Ingredient { get; set; }
    public int Amount { get; set; }

    public SupplyOrder(Ingredient ingredient, int amount)
    {
        Ingredient = ingredient;
        Amount = amount;
    }
    private SupplyOrder() {}
}