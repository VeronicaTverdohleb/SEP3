using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IMenuService
{
    public Task<IEnumerable<Item>> GetItemsByDateAsync(DateTime? date);

}