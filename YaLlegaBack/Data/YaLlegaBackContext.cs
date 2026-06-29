using Microsoft.EntityFrameworkCore;
using YaLlega.Entities;
using YaLlegaBack.Models;
namespace YaLlegaBack.Data
{
    public class YaLlegaBackContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartProductOrder> CartProductOrders { get; set; }
        public YaLlegaBackContext(DbContextOptions<YaLlegaBackContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.EmailAddress)
                .IsUnique();

            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.User)
                .WithOne(u => u.Restaurant)
                .HasForeignKey<Restaurant>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

