using Shared.Model;

namespace Application.DaoInterfaces;

public interface IMenuDao
{
    public Task<IEnumerable<Item>> GetItemsByDateAsync(DateTime date);

}