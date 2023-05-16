using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Model;

namespace EfcDataAccess.DAOs;

public class OrderDao : IOrderDao
{
    private readonly DataContext context;

    public OrderDao(DataContext context)
    {
        this.context = context;
    }
    public Task<IEnumerable<Order>> GetAllOrdersAsync(SearchOrderParametersDto searchParameters)
    {
        IEnumerable<Order> result = context.Orders.Include(order => order.Customer).Include(order=>order.Items).AsEnumerable();

        if (searchParameters.Id!=null)
        {
            result = result.Where(o => o.Id == searchParameters.Id);
        }

        if (searchParameters.Date.HasValue)
        {
            result = result.Where(o => o.Date.ToString().Equals(searchParameters.Date.ToString()));
        }

        if (!string.IsNullOrEmpty(searchParameters.UserName))
        {
            result = result.Where(o => o.Customer.UserName.Equals(searchParameters.UserName));
        }

        if (!string.IsNullOrEmpty(searchParameters.CompletedStatus))
        {
            result = result.Where(o => o.Status.Equals(searchParameters.CompletedStatus));
        }

        return Task.FromResult(result);
        
        /*//IQueryable<Order> orderQuery = context.Orders.AsQueryable();
        IQueryable<Order> orderQuery = context.Orders.Include(order => order.Customer).Include(order=>order.Items).AsQueryable();
        
        List<Order> result = await orderQuery.ToListAsync();
        return result;*/
    }

    public Task<Order?> GetByIdAsync(int id)
    {
        Order? existing = context.Orders.Include(order => order.Customer).Include(order => order.Items).ThenInclude(item => item.Ingredients).FirstOrDefault(o =>
            o.Id == id
        );
        return Task.FromResult(existing);
    }

    public async Task<Order> CreateOrderAsync(MakeOrderDto dto)
    {
        ICollection<Item> items = new List<Item>();
        foreach (int item in dto.ItemIds)
        {
            Item? itemI = context.Items.FirstOrDefault(i => i.Id == item);
            items.Add(itemI);
        }
        
        Order newOrder = new Order(dto.Customer,dto.Date,dto.Status, items);
        EntityEntry<Order> added = await context.Orders.AddAsync(newOrder);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task UpdateOrderAsync(Order orderToUpdate)
    {
        Order? existing = await context.Orders.FirstOrDefaultAsync(order => order.Id == orderToUpdate.Id);

        if (existing == null)
        {
            throw new Exception($"Order with id {orderToUpdate.Id} does not exist!");
        }

        // Update the properties of the existing order
        existing.Status = orderToUpdate.Status;

        // Update the items in the order
        var existingItemIds = existing.Items.Select(item => item.Id).ToList();
        var updatedItemIds = orderToUpdate.Items.Select(item => item.Id).ToList();

        // Remove items that are no longer in the updated order
        foreach (var itemId in existingItemIds.Except(updatedItemIds))
        {
            Item? itemToRemove = await context.Items.FindAsync(itemId);
            if (itemToRemove!=null)
            {
                existing.Items.Remove(itemToRemove);
            }
        }

        // Add new items or update existing items in the order
        foreach (var updatedItem in orderToUpdate.Items)
        {
            Item? existingItem = existing.Items.FirstOrDefault(item => item.Id == updatedItem.Id);

            if (existingItem != null)
            {
                // Update existing item properties
                existingItem.Name = updatedItem.Name;
                existingItem.Price = updatedItem.Price;
            }
            else
            {
                // Add new item to the order
                Item newItem = new Item
                {
                    Id = updatedItem.Id,
                    Name = updatedItem.Name,
                    Price = updatedItem.Price
                };
                existing.Items.Add(newItem);
            }
        }

        await context.SaveChangesAsync();
    }




    public async Task DeleteOrderAsync(int id)
    {
        Order? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Order with id {id} not found");
        }

        context.Orders.Remove(existing);
        await context.SaveChangesAsync();
    }
}