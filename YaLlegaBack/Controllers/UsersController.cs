using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YaLlega.Entities;
using YaLlega.Models;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;


namespace YaLlegaBack.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService UserService)
        {
            _userService = UserService;
        }

        [HttpGet("GetAll")]
        [Authorize]
        public ActionResult<UserDataDto> GetAll()
        {
            IEnumerable<UserDataDto> users = _userService.GetAll();
            if (users?.Any() != true)
            {
                return NoContent();
            }
            else
            {
                return Ok(users);                
            }
        }

        [HttpGet("GetOneByid/{id}")]
        public IActionResult GetOneById(int id)
        {
            if (id == 0)
            {
                return BadRequest("El ID ingresado debe ser distinto de 0");
            }

            GetUserByIdDto? user = _userService.GetById(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("GetOneByEmail/{email}")]
        public IActionResult GetOneByEmail(string email)
        {
            if (email == null)
            {
                return BadRequest("Debe ingresar una dirección de email.");
            }

            GetUserByIdDto? user = _userService.GetByEmail(email);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

 
        [HttpPost("Create")]
        [AllowAnonymous]
        public IActionResult CreateUser([FromBody] CreateUserRequest dto)
        {
            int? newUserId = _userService.Create(dto.User , dto.Restaurant);
            if (newUserId == null)
            {
                return BadRequest("Ya existe un usuario con esos datos.");
            }
            else
            {
                GetUserByIdDto userDto = _userService.GetById((int)newUserId);
                Restaurant restaurant = _userService.GetRestaurant((int)newUserId);
                var response = new
                {
                    Mensaje = "Usuario y restaurante creados correctamente.",
                    Usuario = userDto,
                    Restaurante = restaurant
                };
                return Created("Usuario Creado: ",response);
            }
        }

        [HttpPut("Update")]
        [Authorize]
        public IActionResult UpdateUser(NewUpdatedUserDto updatedUser)
        {
            int userToUpdateId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            ServiceResult result = _userService.Update(updatedUser, userToUpdateId);
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("Delete")]
        [Authorize]
        public IActionResult Delete()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            ServiceResult message = _userService.Delete(userId);
            return StatusCode(message.StatusCode, message.Message);
        }
    }
}
