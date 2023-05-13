using Shared.Dtos;
using Shared.Dtos.SupplyDto;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface ISupplyOrderLogic
{
    //Task<SupplyOrder> CreateAsync(SupplyOrderCreationDto dto);
    Task<IEnumerable<SupplyOrder>> GetAsync();
}