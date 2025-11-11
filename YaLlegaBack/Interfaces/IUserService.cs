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
        public UserDataDto? GetById(int userId);
        public UserDataDto? ValidateUser(AuthDto request);
        public void Update(UpdatedUserDto updatedUser, int userId);
        public void Delete(int userId);
    }
}
