using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface IRestaurantService
    {
        public bool CheckIfRestaurantExists(int restaurantId);
        public bool CheckIfRestaurantNameExists(string name);
        public IEnumerable<GetRestaurantByIdDto> GetAll();
        public GetRestaurantByIdDto? GetById(int userId);
        public ServiceResult Delete(int restaurantId);
        public int? Create(NewUpdatedRestaurantDTO newRestaurant, int? userId);
        public bool? RestaurantIsOpen(int userId);
    }
}
