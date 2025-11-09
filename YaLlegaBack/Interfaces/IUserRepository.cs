using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaLlega.Entities;
using YaLlega.Models;
using YaLlega1.Models;

namespace YaLlega.Interfaces
{
    internal interface IUserRepository
    {
        public void Create(User newUser);

        public bool CheckIfUserExists(int userId);

        public List<User> GetAll();

        public User? GetById(int userId);
        public User? ValidateUser(AuthDto request);
        public void Update(User updatedUser, int userId);
        public void Delete(int userId);
    }
}
