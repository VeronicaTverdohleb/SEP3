﻿using Shared.Dtos;
using Shared.Model;

namespace HttpClients.ClientInterfaces;
/// <summary>
/// Interface implemented by OrderService
/// </summary>
public interface IOrderService
{
    Task<ICollection<Order>> getAllOrdersAsync(int? userId, DateOnly? date, string? userName, string? completedStatus);
    Task<OrderFullInfoDto> GetOrderByIdAsync(int id);
    Task UpdateAsync(OrderUpdateDto dto);
    Task CreateAsync(MakeOrderDto dto);
    Task DeleteAsync(int id);

}