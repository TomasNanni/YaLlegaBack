using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using YaLlega.Entities;
using YaLlegaBack.Data;
using YaLlegaBack.Interfaces;

namespace YaLlegaBack.Repositories
{
    public class RestaurantRepostory : IRestaurantRepository
    {
        private YaLlegaBackContext _context;

        public RestaurantRepostory(YaLlegaBackContext context)
        {
            _context = context;
        }
        public bool CheckIfRestaurantExists(int userId)
        {
            return _context.Restaurants.Any(restaurant=> restaurant.UserId == userId);
        }
        public bool? RestaurantIsOpen (int userId)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(restaurant => restaurant.UserId == userId);

            if (restaurant == null)
            {
                return null;
            }
            string today = DateTime.Now.DayOfWeek.ToString().ToLower();
            if (!restaurant.OpenDays.Any(day => day.ToLower() == today))
            {
                return false;
            }
            var hour = TimeOnly.FromDateTime(DateTime.Now);
            return hour >= restaurant.OpeningTime && hour <= restaurant.ClosingTime;
        }

        public int Create(Restaurant newRestaurant)
        {
            var createdRestaurant = _context.Restaurants.Add(newRestaurant).Entity;
            _context.SaveChanges();
            return createdRestaurant.UserId;
        }

        public void Delete(int userId)
        {
            _context.Restaurants.Remove(_context.Restaurants.Single(restaurant => restaurant.UserId == userId));
            _context.SaveChanges();
            return;
        }

        public List<Category>? GetCategory(int userId)
        {
            var restaurants = _context.Restaurants.FirstOrDefault(restaurant => restaurant.UserId == userId);
            if (restaurants == null)
            {
                return null;
            }
            else
            {
                return restaurants.Categories.ToList();
            }
        }

        public List<Restaurant> GetAll()
        {
            return _context.Restaurants.ToList();
        }

        public Restaurant? GetById(int userId)
        {
            return _context.Restaurants.FirstOrDefault(restaurant => restaurant.UserId == userId);
        }

        public bool CheckIfRestaurantNameExists (string name)
        {
            return _context.Restaurants.Any(restaurant => restaurant.Name == name);
        }

        public void Update(Restaurant updatedRestaurant, int userId)
        {
            var restaurantToEdit = _context.Restaurants.First(u => u.UserId == userId);
            restaurantToEdit.Name = updatedRestaurant.Name;
            restaurantToEdit.UrlLogoImage = updatedRestaurant.UrlLogoImage;
            restaurantToEdit.UrlBannerImage = updatedRestaurant.UrlBannerImage;
            restaurantToEdit.OpenDays = updatedRestaurant.OpenDays;
            restaurantToEdit.OpeningTime = updatedRestaurant.OpeningTime;
            restaurantToEdit.ClosingTime = updatedRestaurant.ClosingTime;
            restaurantToEdit.Contact = updatedRestaurant.Contact;
            _context.SaveChanges();
            return;
        }
    }
}
