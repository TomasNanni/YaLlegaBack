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
        public ServiceResult Update(UpdatedUserDto updatedUser, int userId);
        public ServiceResult Delete(int userId);
    }
}
