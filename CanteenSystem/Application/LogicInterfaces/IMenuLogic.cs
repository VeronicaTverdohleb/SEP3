using Shared.Model;

namespace Application.LogicInterfaces;

public interface IMenuLogic
{
    public Task<IEnumerable<Item>> GetItemsByDateAsync(DateTime date);
}