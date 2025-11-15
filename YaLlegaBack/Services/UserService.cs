using System.Runtime.CompilerServices;
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

        public int? Create(NewUserDataDTO newUser)
        {
            if (_userRepository.GetByEmail(newUser.EmailAdress) == null)
            {
                return null;
            }
            var user = new User()
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                EmailAddress = newUser.EmailAdress,
                Password = newUser.Password,
                Restaurant = newUser.Restaurant,
            };
            var newUserId = _userRepository.Create(user);
            return newUserId;
        }

        public string Delete(int userId)
        {
            if (CheckIfUserExists(userId))
            {
                var user = GetById(userId);
                Restaurant restaurant = user.Restaurant;
                int restaurantId = restaurant.Id;
                if (_restaurantService.CheckIfRestaurantExists(restaurantId))
                {
                    _restaurantService.Delete(restaurantId);
                    _userRepository.Delete(userId);
                    return ("Usuario y restaurante borrados correctamente.");
                }
                else
                {
                    return ("El usuario no tiene un restaurante asociado.");
                }
            }
            else
            {
                return ("El usuario no existe.");
            }
        }

        public IEnumerable<UserDataDto> GetAll()
        {

            var users = _userRepository.GetAll();
            IEnumerable<UserDataDto> usersData = new List<UserDataDto>();
            foreach (var user in users)
            {
                usersData.
            }
        }

        public UserDataDto? GetById(int userId)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdatedUserDto updatedUser, int userId)
        {
            throw new NotImplementedException();
        }

        public UserDataDto? ValidateUser(AuthDto request)
        {
            throw new NotImplementedException();
        }
    }
}
