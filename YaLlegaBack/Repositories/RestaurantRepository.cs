using Microsoft.EntityFrameworkCore;
using YaLlega.Entities;
using YaLlegaBack.Data;
using YaLlegaBack.Interfaces;

namespace YaLlegaBack.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private YaLlegaBackContext _context;

        public RestaurantRepository(YaLlegaBackContext context)
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
            if (hour >= restaurant.OpeningTime && hour <= restaurant.ClosingTime)
            {
                return true;
            }
            else
            {
                return false;
            }
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
    }
}
