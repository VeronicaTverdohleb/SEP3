using Shared.Dtos;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IMenuLogic
{
    public Task<Menu> GetMenuByDateAsync(DateTime date);
}