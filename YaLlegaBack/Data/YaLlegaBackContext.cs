using Microsoft.EntityFrameworkCore;
using YaLlega.Entities;
namespace YaLlegaBack.Data
{
    public class YaLlegaBackContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public YaLlegaBackContext(DbContextOptions<YaLlegaBackContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

