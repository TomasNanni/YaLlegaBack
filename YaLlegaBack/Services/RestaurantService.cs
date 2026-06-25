using YaLlega.Entities;
using YaLlega.Interfaces;
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
            if (newRestaurant == null)
            {
                throw new ArgumentException("Los datos del restaurante no pueden estar vacíos");
            }
            if (userId == null || userId <= 0)
            {
                throw new ArgumentException("El id del usuario debe ser válido");
            }
            if (string.IsNullOrWhiteSpace(newRestaurant.Name))
            {
                throw new ArgumentException("El nombre del restaurante no puede estar vacío");
            }
            if (CheckIfRestaurantNameExists(newRestaurant.Name) == true)
            {
                throw new ArgumentException("Ya existe un restaurante con ese nombre");
            }
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
            if (newRestaurantId <= 0)
            {
                throw new ArgumentException("No se pudo crear el restaurante");
            }
            return newRestaurantId;
        }

        public ServiceResult Delete(int restaurantId)
        {
            if (restaurantId <= 0)
            {
                throw new ArgumentException("El id del restaurante debe ser mayor a 0");
            }
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
                throw new ArgumentException("No existe restaurante con el id ingresado.");
            }
        }

        public IEnumerable<GetRestaurantByIdDto> GetAll()
        {
            return _restaurantRepository.GetAll().Select(restaurant =>
            new GetRestaurantByIdDto
            {
                Id = restaurant.UserId,
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
            if (restaurantId <= 0)
            {
                throw new ArgumentException("El id del restaurante debe ser mayor a 0");
            }
            if (updatedRestaurant == null)
            {
                throw new ArgumentException("Los datos del restaurante no pueden estar vacíos");
            }
            if (string.IsNullOrWhiteSpace(updatedRestaurant.Name))
            {
                throw new ArgumentException("El nombre del restaurante no puede estar vacío");
            }
            if (CheckIfRestaurantNameExists(updatedRestaurant.Name) == true)
            {
                throw new ArgumentException("El nombre del restaurante ya existe.");
            }
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
        public bool? RestaurantIsOpen (int userId)
        {
            return _restaurantRepository.RestaurantIsOpen(userId);
        }
    }
}
