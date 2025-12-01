using YaLlega.Entities;

namespace YaLlegaBack.Interfaces
{
    public interface IRestaurantRepository
    {
        public bool CheckIfRestaurantExists(int restaurantId);
        public List<Restaurant> GetAll();

    }
}
