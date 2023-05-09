using Microsoft.EntityFrameworkCore;
using Shared.Model;


namespace EfcDataAccess;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Menu> Menus { get; set; }

    public DbSet<SupplyOrder> SupplyOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../DataAccess/VIACanteen.db")
            .EnableSensitiveDataLogging();
    }
    
    // This method configures property constraints in the database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id); // Setting Id to be the primary key
        modelBuilder.Entity<Item>().HasKey(item => item.Id);
        modelBuilder.Entity<Ingredient>().HasKey(ingredient => ingredient.Id);
        modelBuilder.Entity<SupplyOrder>().HasKey(supplyOrder => supplyOrder.Id);
        modelBuilder.Entity<Order>().HasKey(order => order.Id);
        modelBuilder.Entity<Menu>().HasKey(dailyMenu => dailyMenu.Date);
    }
    
}