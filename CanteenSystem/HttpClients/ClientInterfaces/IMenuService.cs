using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IMenuService
{
    public Task<IEnumerable<Item>> GetItemsByDate(DateTime? date);

}