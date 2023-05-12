using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Model;

namespace EfcDataAccess.DAOs;

public class SupplyOrderDao : ISupplyOrderDao
{
    private readonly DataContext context;
    
    public SupplyOrderDao(DataContext context)
    {
        this.context = context;
    }
    public async Task<SupplyOrder> CreateAsync(SupplyOrder supplyOrder)
    {
        EntityEntry<SupplyOrder> added = await context.SupplyOrders.AddAsync(supplyOrder);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<SupplyOrder>> GetAsync()
    {
        IEnumerable<SupplyOrder> list = context.SupplyOrders.ToList();
        return list;
    }
/*
    public async Task<Supplier?> GetSupplierByName(string name)
    {
        Supplier? found = await context.Suppliers
            .AsNoTracking().SingleOrDefaultAsync(i => i.SupplierName == name);
        return found;
    }
    */
}