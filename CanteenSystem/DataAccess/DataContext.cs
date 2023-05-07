using Microsoft.EntityFrameworkCore;
using Shared.Model;
using SupplyOrder = Application.Logic.SupplyOrder;

namespace EfcDataAccess;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    //public DbSet<Item> Items { get; set; }
    //public DbSet<Ingredient> Ingredients { get; set; }
    //public DbSet<SupplyOrder> SupplyOrders { get; set; }
    //public DbSet<Order> Orders { get; set; }
    
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../DataAccess/VIACanteen.db")
            .EnableSensitiveDataLogging();
    }
    
    // This method configures property constraints in the database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id);    // Setting Id to be the primary key
    }
    
}