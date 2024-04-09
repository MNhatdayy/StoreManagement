using Microsoft.EntityFrameworkCore;
using StoreManagement.Models;

namespace StoreManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ): base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuDetail> MenuDetails { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuDetail>()
                 .HasKey(m => new { m.FoodItemId, m.MenuId });

                .HasKey(m=> new { m.OderId, m.FoodId}

            modelBuilder.Entity<FoodCategory>()
                .HasOne(fc => fc.Store)
                .WithMany(s => s.FoodCategories)
                .HasForeignKey(fc => fc.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(o => o.FoodItem)
                .WithMany()
                .HasForeignKey(o => o.FoodId);
            
        }

    }
}
