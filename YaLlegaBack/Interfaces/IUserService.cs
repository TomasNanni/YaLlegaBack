using YaLlega.Entities;
using YaLlega.Models;
using YaLlega1.Models;
using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface IUserService
    {
        public int? Create(NewUserDataDTO newUser);
        public bool CheckIfUserExists(int userId);
        public IEnumerable<UserDataDto> GetAll();
        public GetUserByIdDto? GetById(int userId);
        public GetUserByIdDto? GetByEmail(string email);
        public UserDataDto? ValidateUser(AuthDto request);
        public UsersServiceResult Update(UpdatedUserDto updatedUser, int userId);
        public UsersServiceResult Delete(int userId);
    }
}
