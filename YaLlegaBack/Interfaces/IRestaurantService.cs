using YaLlega.Entities;
using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface IRestaurantService
    {
        public bool CheckIfRestaurantExists(int restaurantId);
        public bool CheckIfRestaurantNameExists(string name);
        public IEnumerable<RestaurantDataDto> GetAll();
        public GetRestaurantByIdDto? GetById(int restaurantId);
        public ServiceResult Update(NewUpdatedRestaurantDTO updatedRestaurant, int restaurantId);
        public ServiceResult Delete(int restaurantId);
        public int? Create(NewUpdatedRestaurantDTO newRestaurant, int? userId);
    }
}
