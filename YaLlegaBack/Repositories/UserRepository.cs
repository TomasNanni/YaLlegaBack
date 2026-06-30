using YaLlega.Entities;
using YaLlega.Interfaces;
using YaLlegaBack.Data;

namespace YaLlega.Repositories
{
    public class UserRepository : IUserRepository
    {
        private YaLlegaBackContext _context;

        public UserRepository(YaLlegaBackContext context)
        {
            _context = context;
        }
        public int Create(User newUser)
        {
            var createdUser = _context.Users.Add(newUser).Entity;
            _context.SaveChanges();
            return createdUser.Id;
        }

        public bool CheckIfUserExists(int userId)
        {
            return _context.Users.Any(user => user.Id == userId);
        }



        public User? GetById(int userId)
        {
            return _context.Users.FirstOrDefault(user => user.Id == userId);
        }
        public User? GetByEmail(string userEmail)
        {
            return _context.Users.FirstOrDefault(user => user.EmailAddress.ToLower() == userEmail.ToLower());
        }

        public void Update(User updatedUser, int userId)
        {
            var userToEdit = _context.Users.First(u => u.Id == userId);
            if (updatedUser.FirstName != null) userToEdit.FirstName = updatedUser.FirstName;
            if (updatedUser.LastName != null) userToEdit.LastName = updatedUser.LastName;
            if (updatedUser.EmailAddress != null) userToEdit.EmailAddress = updatedUser.EmailAddress;
            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            _context.Users.Remove(_context.Users.Single(user => user.Id == userId));
            _context.SaveChanges();
        }

        public Restaurant? GetRestaurant(int userId)
        {
            return _context.Restaurants.FirstOrDefault(r => r.UserId == userId);
        }
    }
}
