using YaLlega.Interfaces;
using YaLlega.Models;
using YaLlega1.Models;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
        private readonly RestaurantService _restaurantService;

        public UserService(IUserRepository _userRepository, RestaurantService _restaurantService)
        {
            _userRepository = _userRepository;
            _restaurantService = _restaurantService;
        }
        public bool CheckIfUserExists(int userId)
        {
            throw new NotImplementedException();
        }

        public void Create(NewUserDataDTO newUser)
        {
            throw new NotImplementedException();
        }

        public void Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDataDto> GetAll()
        {
            throw new NotImplementedException();
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
