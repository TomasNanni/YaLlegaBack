using YaLlega.Entities;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Services
{
    public class RestaurantService : IRestaurantService
    {
        public bool CheckIfRestaurantExists(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public int? Create(NewUpdatedRestaurantDTO newRestaurant)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Delete(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RestaurantDataDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public Restaurant? GetById(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Update(NewUpdatedRestaurantDTO updatedRestaurant, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
