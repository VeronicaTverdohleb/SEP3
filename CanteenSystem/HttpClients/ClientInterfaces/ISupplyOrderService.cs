using Shared.Dtos;
using Shared.Dtos.SupplyDto;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface ISupplyOrderService
{
    Task CreateAsync(SupplyOrderCreationDto dto);
    
    Task<ICollection<SupplyOrder>> GetAsync();
    
    //Task<Supplier?> GetSupplierByName(string name);
}