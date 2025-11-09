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

            Restaurant mcdonald = new Restaurant()
            {
                Id = 1,
                UserId = 1,
                Name = "mcdonald",
                OpeningTime = new TimeOnly(9, 0, 0),
                ClosingTime = new TimeOnly(22, 0, 0),
                UrlLogoImage = "string",
                UrlBannerImage = "string",
                OpenDays = "mondaytuesday",
                Contact = "+54",
                User = tomas,
            };

            User tomas = new User()
            {
                FirstName = "tomas",
                LastName = "nanni",
                EmailAdress = "tomas@gmail.com",
                Password = "contraseña",
                Restaurant = mcdonald,
            };


            modelBuilder.Entity<User>().HasData(tomas);
            modelBuilder.Entity<Restaurant>().HasData(mcdonald);

            base.OnModelCreating(modelBuilder);
        }
    }
}

