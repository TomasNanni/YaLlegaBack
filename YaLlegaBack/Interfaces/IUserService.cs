using YaLlega.Entities;
using YaLlega.Models;
using YaLlega1.Models;
using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface IUserService
    {
        public int? Create(NewUpdatedUserDto newUser, NewUpdatedRestaurantDTO newRestaurantData);
        public bool CheckIfUserExists(int userId);
        public IEnumerable<UserDataDto> GetAll();
        public GetUserByIdDto? GetById(int userId);
        public GetUserByIdDto? GetByEmail(string email);
        public UserDataDto? ValidateUser(AuthDto request);
        public ServiceResult Update(NewUpdatedUserDto updatedUser, int userId);
        public ServiceResult Delete(int userId);
        public User? Authenticate(string email, string password);
        public Restaurant? GetRestaurant(int userId);
    }
}
