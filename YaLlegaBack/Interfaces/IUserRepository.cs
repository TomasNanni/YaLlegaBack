using YaLlega.Entities;

namespace YaLlega.Interfaces
{
    public interface IUserRepository
    {
        public int Create(User newUser);
        public bool CheckIfUserExists(int userId);
        public Restaurant? GetRestaurant(int userId);
        public User? GetById(int userId);
        public User? GetByEmail(string userEmail);
        public void Update(User updatedUser, int userId);
        public void Delete(int userId);
    }
}
