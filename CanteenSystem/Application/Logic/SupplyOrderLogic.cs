using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Dtos.SupplyDto;
using Shared.Model;

namespace Application.Logic;

public class SupplyOrderLogic : ISupplyOrderLogic
{
    private readonly ISupplyOrderDao supplyOrderDao;
    private readonly IIngredientDao ingredientDao;

    public SupplyOrderLogic(ISupplyOrderDao supplyOrderDao, IIngredientDao ingredientDao)
    {
        this.supplyOrderDao = supplyOrderDao;
        this.ingredientDao = ingredientDao;
    }

    /*
    public async Task<SupplyOrder> CreateAsync(SupplyOrderCreationDto dto)
    {
        Ingredient? ingredient = await ingredientDao.GetByIdAsync(dto.IngredientId);
        if (ingredient == null)
        {
            throw new Exception("Ingredient not found!");
        }

        Supplier? supplier = await supplyOrderDao.GetSupplierByName(dto.SupplierName);
        if (supplier == null)
        {
            throw new Exception("Supplier not found!");
        }
        
        SupplyOrder todo = new SupplyOrder(ingredient, dto.Amount, supplier); 
        SupplyOrder created = await supplyOrderDao.CreateAsync(todo); 
        
        return created;
    }
    */
    
    public Task<IEnumerable<SupplyOrder>> GetAsync()
    {
        return supplyOrderDao.GetAsync();
    }
}