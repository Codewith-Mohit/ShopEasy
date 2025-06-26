using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopEasy.API.Models;

namespace ShopEasy.API.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        // public DbSet<User> Users => Set<User>();

        public DbSet<Product> Products => Set<Product>();

        public DbSet<Cart> Carts => Set<Cart>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<CartItem> cartItems => Set<CartItem>();
    }
}
