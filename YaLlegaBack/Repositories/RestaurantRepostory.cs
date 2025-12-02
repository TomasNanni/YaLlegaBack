using Microsoft.EntityFrameworkCore;
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
        public bool CheckIfRestaurantExists(int restaurantId)
        {
            return _context.Restaurants.Any(restaurant=> restaurant.Id == restaurantId);
        }

        public int Create(Restaurant newRestaurant)
        {
            var createdRestaurant = _context.Restaurants.Add(newRestaurant).Entity;
            _context.SaveChanges();
            return createdRestaurant.Id;
        }

        public void Delete(int restaurantId)
        {
            _context.Restaurants.Remove(_context.Restaurants.Single(restaurant => restaurant.Id == restaurantId));
            _context.SaveChanges();
            return;
        }

        public List<Restaurant> GetAll()
        {
            return _context.Restaurants.ToList();
        }

        public Restaurant? GetById(int restaurantId)
        {
            return _context.Restaurants.FirstOrDefault(restaurant => restaurant.Id == restaurantId);
        }

        public void Update(Restaurant updatedRestaurant, int restaurantId)
        {
            var restaurantToEdit = _context.Restaurants.First(u => u.Id == restaurantId);
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
