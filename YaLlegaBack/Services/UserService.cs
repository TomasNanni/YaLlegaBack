using Humanizer;
using System.Globalization;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using YaLlega.Entities;
using YaLlega.Interfaces;
using YaLlega.Models;
using YaLlega.Repositories;
using YaLlega1.Models;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IRestaurantService _restaurantService;

        public UserService(IUserRepository userRepository, IRestaurantService restaurantService)
        {
            _userRepository = userRepository;
            _restaurantService = restaurantService;
        }
        public bool CheckIfUserExists(int userId)
        {
            return _userRepository.CheckIfUserExists(userId);
        }

        public int? Create(NewUpdatedUserDto newUser, NewUpdatedRestaurantDTO newRestaurantData)
        {
            if (newUser == null)
            {
                throw new ArgumentException("Los datos del usuario no pueden estar vacíos");
            }
            if (newRestaurantData == null)
            {
                throw new ArgumentException("Los datos del restaurante no pueden estar vacíos");
            }
            if (string.IsNullOrWhiteSpace(newUser.EmailAddress) || !IsValidEmail(newUser.EmailAddress))
            {
                throw new ArgumentException("El email no es válido");
            }
            if (_userRepository.GetByEmail(newUser.EmailAddress) != null)
            {
                throw new ArgumentException("Ya existe un usuario con ese email");
            }
            var user = new User()
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                EmailAddress = newUser.EmailAddress,
                Password = newUser.Password,
            };
            int? newUserId = _userRepository.Create(user);
            if (newUserId == null || newUserId <= 0)
            {
                throw new ArgumentException("No se pudo crear el usuario");
            }
            var newRestaurantId = _restaurantService.Create(newRestaurantData, newUserId);
            if (newRestaurantId == null)
            {
                _userRepository.Delete((int)newUserId);
                throw new ArgumentException("No se pudo crear el restaurante");
            }
            else
            {
                Restaurant? restaurant = _userRepository.GetRestaurant((int)newUserId);
                user.Restaurant = restaurant;
                _userRepository.Update(user, (int)newUserId);
                return newUserId;
            }
        }

        public ServiceResult Delete(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("El id del usuario debe ser mayor a 0");
            }
            if (CheckIfUserExists(userId))
            {
                //Borra user y restaurante en cascada
                var restaurant = GetRestaurant(userId);
                ServiceResult result = _restaurantService.Delete(restaurant.UserId);
                if (result.StatusCode == 200)
                {
                    _userRepository.Delete(userId);
                    return new ServiceResult
                    {
                        Message = "Usuario y restaurante borrados correctamente.",
                        StatusCode = 200,
                    };
                }
                else
                {
                    return new ServiceResult
                    {
                        Message = "Usuario borrado correctamente, restaurante no.",
                        StatusCode = 200,
                    };
                }
            }
            else
            {
                throw new ArgumentException("El usuario no existe.");
            }
        }

        public IEnumerable<UserDataDto> GetAll()
        {
            return _userRepository.GetAll().Select(user =>
            new UserDataDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Restaurant = user.Restaurant,
            });
        }

        public GetUserByIdDto? GetById(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user != null)
            {
                return new GetUserByIdDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                };
            }
            else
            {
                return null;
            }
        }

        public GetUserByIdDto? GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user != null)
            {
                return new GetUserByIdDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                };
            }
            else
            {
                return null;
            }
        }

        public ServiceResult Update(NewUpdatedUserDto updatedUser, int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("El id del usuario debe ser mayor a 0");
            }
            if (updatedUser == null)
            {
                throw new ArgumentException("Los datos del usuario no pueden estar vacíos");
            }
            if (CheckIfUserExists(userId) == false)
            {
                throw new ArgumentException("El usuario que intentó actualizar no existe.");
            }
            if (string.IsNullOrWhiteSpace(updatedUser.EmailAddress) || IsValidEmail(updatedUser.EmailAddress) == false)
            {
                throw new ArgumentException("La dirección de email no existe o no es válida.");
            }
            if (GetByEmail(updatedUser.EmailAddress) != null)
            {
                throw new ArgumentException("Ya existe un usuario con la dirección de correo ingresada.");
            }
            User user = new User
            {
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                EmailAddress = updatedUser.EmailAddress,
            };
            _userRepository.Update(user, userId);
            return new ServiceResult
            {
                Message = "Usuario actualizado correctamente.",
                StatusCode = 200,
            };
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public UserDataDto? ValidateUser(AuthDto request)
        {
            var user = _userRepository.ValidateUser(request);
            if (user == null)
            {
                return null;
            }
            else
            {
                return new UserDataDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                };
            }
        }

        public User? Authenticate(string email, string password)
        {
            email = email.Trim().ToLower();

            var user = _userRepository.GetByEmail(email);

            if (user is null)
                return null;

            if (user.Password == password)
                return user;

            return null;
        }
        public Restaurant? GetRestaurant(int userId)
        {
            return _userRepository.GetRestaurant(userId);
        }
        public GetRestaurantByIdDto? GetRestaurantDto(int userId)
        {
            Restaurant restaurant = _userRepository.GetRestaurant(userId);
            return new GetRestaurantByIdDto
            {
                Name = restaurant.Name,
                Contact = restaurant.Contact,
                ClosingTime = restaurant.ClosingTime,
                OpeningTime = restaurant.OpeningTime,
                OpenDays = restaurant.OpenDays,
                UrlBannerImage = restaurant.UrlBannerImage,
                UrlLogoImage = restaurant.UrlLogoImage,
            };
        }
    }
}
