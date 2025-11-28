using Humanizer;
using System.Globalization;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using YaLlega.Entities;
using YaLlega.Interfaces;
using YaLlega.Models;
using YaLlega.Repositories;
using YaLlega1.Models;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
        private readonly IRestaurantService _restaurantService;

        public UserService(IUserRepository userRepository, IRestaurantService restaurantService)
        {
            _userRepository = userRepository;
            _restaurantService = restaurantService;
        }
        public bool CheckIfUserExists(int userId)
        {
            return _userRepository.CheckIfUserExists(userId);
        }

        public int? Create(NewUserDataDTO newUser)
        {
            if (_userRepository.GetByEmail(newUser.EmailAddress) != null)
            {
                return null;
            }
            var user = new User()
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                EmailAddress = newUser.EmailAddress,
                Password = newUser.Password,
                //Restaurant = newUser.Restaurant,
            };
            var newUserId = _userRepository.Create(user);
            return newUserId;
        }

        public string Delete(int userId)
        {
            if (CheckIfUserExists(userId))
            {
                _userRepository.Delete(userId);
                return ("Usuario y restaurante borrados correctamente.");
            }
            else
            {
                return ("El usuario no existe.");
            }
        }

        public IEnumerable<UserDataDto> GetAll()
        {
            return _userRepository.GetAll().Select(user =>
            new UserDataDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Restaurant = user.Restaurant,
            });
        }

        public GetUserByIdDto? GetById(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user != null)
            {
                return new GetUserByIdDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                };
            }
            else
            {
                return null;
            }
        }

        public GetUserByIdDto? GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user != null)
            {
                return new GetUserByIdDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                };
            }
            else
            {
                return null;
            }
        }

        public string Update(UpdatedUserDto updatedUser, int userId)
        {
            if (CheckIfUserExists(userId) == false)
            {
                return ("El usuario que quizo actualizar no existe.");
            }
            if (string.IsNullOrWhiteSpace(updatedUser.EmailAdress) || IsValidEmail(updatedUser.EmailAdress) == false) 
            {
                return ("La dirección de email no existe o no es valida.");
            }
            if (GetByEmail(updatedUser.EmailAdress) != null)
            {
                return ("Ya existe un usuario con la dirección de correo ingresada.");
            }
            User user = new User
            {
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                EmailAddress = updatedUser.EmailAdress,
            };
            _userRepository.Update(user, userId);
            return ("Usuario actualizado correctamente");
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public UserDataDto? ValidateUser(AuthDto request)
        {
            var user = _userRepository.ValidateUser(request);
            if (user == null)
            {
                return null;
            }
            else
            {
                return new UserDataDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                };
            }
        }
    }
}
