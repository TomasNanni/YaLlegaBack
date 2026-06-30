using YaLlega.Entities;
using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface IUserService
    {
        public int? Create(NewUserDto newUser, NewUpdatedRestaurantDTO newRestaurantData);
        public bool CheckIfUserExists(int userId);
        public GetUserByIdDto? GetById(int userId);
        public GetUserByIdDto? GetByEmail(string email);
        public ServiceResult Update(UpdateUserDto updatedUser, int userId);
        public ServiceResult Delete(int userId);
        public int? Authenticate(string email, string password);
        public Restaurant? GetRestaurant(int userId);
        public GetRestaurantByIdDto? GetRestaurantDto(int userId);
    }
}
