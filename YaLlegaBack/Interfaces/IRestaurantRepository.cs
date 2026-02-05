using YaLlega.Entities;

namespace YaLlegaBack.Interfaces
{
    public interface IRestaurantRepository
    {
        public bool CheckIfRestaurantExists(int restaurantId);
        public List<Restaurant> GetAll();
        public Restaurant? GetById(int restaurantId);
        public bool CheckIfRestaurantNameExists(string name);
        public bool? RestaurantIsOpen(int userId);
        public void Update(Restaurant updatedRestaurant, int restaurantId);
        public void Delete(int restaurantId);
        public int Create(Restaurant newRestaurant);
        public List<Category>? GetCategory(int restaurantId);
    }
}
