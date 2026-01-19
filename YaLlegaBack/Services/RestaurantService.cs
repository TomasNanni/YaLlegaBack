using Humanizer;
using YaLlega.Entities;
using YaLlega.Interfaces;
using YaLlega.Repositories;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Services
{
    public class RestaurantService : IRestaurantService
    {

        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        public bool CheckIfRestaurantExists(int restaurantId)
        {
            return _restaurantRepository.CheckIfRestaurantExists(restaurantId);
        }
        public bool CheckIfRestaurantNameExists(string name)
        {
            return _restaurantRepository.CheckIfRestaurantNameExists(name);
        }
        public int? Create(NewUpdatedRestaurantDTO newRestaurant, int? userId)
        {
            if (CheckIfRestaurantNameExists(newRestaurant.Name) == false)
            {
                var restaurant = new Restaurant
                {
                    Name = newRestaurant.Name,
                    UrlLogoImage = newRestaurant.UrlLogoImage,
                    UrlBannerImage = newRestaurant.UrlBannerImage,
                    OpenDays = newRestaurant.OpenDays,
                    OpeningTime = newRestaurant.OpeningTime,
                    ClosingTime = newRestaurant.ClosingTime,
                    Contact = newRestaurant.Contact,
                    UserId = (int)userId,
                };
                int newRestaurantId = _restaurantRepository.Create(restaurant);
                return newRestaurantId;
            }
            return null;
        }

        public ServiceResult Delete(int restaurantId)
        {
            if (CheckIfRestaurantExists(restaurantId))
            {
                _restaurantRepository.Delete(restaurantId);
                return new ServiceResult
                {
                    Message = "Restaurante borrado correctamente.",
                    StatusCode = 200,
                };
            }
            else
            {
                return new ServiceResult
                {
                    Message = "No existe restaurante con el id ingresado.",
                    StatusCode = 404,
                };
            }
        }

        public IEnumerable<RestaurantDataDto> GetAll()
        {
            return _restaurantRepository.GetAll().Select(restaurant =>
            new RestaurantDataDto
            {
                Name = restaurant.Name,
                UrlLogoImage = restaurant.UrlLogoImage,
                UrlBannerImage = restaurant.UrlBannerImage,
                OpenDays = restaurant.OpenDays,
                OpeningTime = restaurant.OpeningTime,
                ClosingTime = restaurant.ClosingTime,
                Contact = restaurant.Contact,
            });
        }

        public GetRestaurantByIdDto? GetById(int restaurantId)
        {
            var restaurant = _restaurantRepository.GetById(restaurantId);
            if (restaurant != null)
            {
                return new GetRestaurantByIdDto
                {
                    Name = restaurant.Name,
                    UrlLogoImage = restaurant.UrlLogoImage,
                    UrlBannerImage = restaurant.UrlBannerImage,
                    OpenDays = restaurant.OpenDays,
                    OpeningTime = restaurant.OpeningTime,
                    ClosingTime = restaurant.ClosingTime,
                    Contact = restaurant.Contact
                };
            }
            else
            {
                return null;
            }
        }

        public ServiceResult Update(NewUpdatedRestaurantDTO updatedRestaurant, int restaurantId)
        {
            if (CheckIfRestaurantNameExists(updatedRestaurant.Name) == false)
            {
                var restaurant = new Restaurant
                {
                    Name = updatedRestaurant.Name,
                    UrlLogoImage = updatedRestaurant.UrlLogoImage,
                    UrlBannerImage = updatedRestaurant.UrlBannerImage,
                    OpenDays = updatedRestaurant.OpenDays,
                    OpeningTime = updatedRestaurant.OpeningTime,
                    ClosingTime = updatedRestaurant.ClosingTime,
                    Contact = updatedRestaurant.Contact,
                };
                _restaurantRepository.Update(restaurant, restaurantId);
                return new ServiceResult
                {
                    Message = "Restaurante actualizado correctamente",
                    StatusCode = 200,
                };
            }
            return new ServiceResult
            {
                Message = "El nombre del restaurante ya existe.",
                StatusCode = 400,
            };
        }
    }
}
