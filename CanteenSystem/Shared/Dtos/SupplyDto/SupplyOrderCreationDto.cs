namespace Shared.Dtos.SupplyDto;

public class SupplyOrderCreationDto
{
    public int IngredientId { get; }
    public int Amount { get; set; }
    public string SupplierName { get; }

    public SupplyOrderCreationDto(int ingredientId, int amount, string supplierName)
    {
        IngredientId = ingredientId;
        Amount = amount;
        SupplierName = supplierName;
    }
}