using Microsoft.AspNetCore.Routing.Constraints;
using YaLlega.Entities;
using YaLlega.Interfaces;
using YaLlega1.Models;
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



        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetById(int userId)
        {
            return _context.Users.FirstOrDefault(user => user.Id == userId);
        }
        public User? GetByEmail(string userEmail)
        {
            return _context.Users.FirstOrDefault(user => user.EmailAddress.ToLower() == userEmail.ToLower());
        }

        public User? ValidateUser(AuthDto request)
        {
            return _context.Users.FirstOrDefault(user => user.EmailAddress == request.EmailAddress && user.Password == request.Password);
        }

        public void Update(User updatedUser, int userId)
        {
            var userToEdit = _context.Users.First(u => u.Id == userId);
            userToEdit.FirstName = updatedUser.FirstName;
            userToEdit.EmailAddress = updatedUser.EmailAddress;
            userToEdit.LastName = updatedUser.LastName;
            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            _context.Users.Remove(_context.Users.Single(user => user.Id == userId));
            _context.SaveChanges();
        }
    }
}
