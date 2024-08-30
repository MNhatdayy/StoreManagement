using Microsoft.EntityFrameworkCore;
using Sneaker_Ecommerce.Domain.Entity;

namespace Sneaker_ECommerce.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }
        public virtual DbSet<WishlistItem> WishlistItems { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
    }
}
