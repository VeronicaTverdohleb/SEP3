using Shared.Model;

namespace Application.DaoInterfaces;

public interface ISupplyOrderDao
{
    Task<SupplyOrder> CreateAsync(SupplyOrder supplyOrder);
    Task<IEnumerable<SupplyOrder>> GetAsync();
   // Task<Supplier?> GetSupplierByName(string name);

}