using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateUser([FromBody] NewUserDataDTO dto)
        {
            int? newUserId = _userService.Create(dto);
            if (newUserId == null)
            {
                return BadRequest("Ya existe un usuario con esos datos.");
            }
            else
            {
                var user = GetOneById((int)newUserId);
                return Created("Usuario creado con los datos: ",user);
            }
        }

        [HttpPut("Update")]
        public IActionResult UpdateUser(UpdatedUserDto updatedUser ,int userToUpdateId)
        {
            string message = _userService.Update(updatedUser ,userToUpdateId);
            return message switch
            {
                "Usuario actualizado correctamente." => Ok(message),
                "El usuario que quiso actualizar no existe." => NotFound(message),
                "La dirección de email no existe o no es válida." => BadRequest(message),
                "Ya existe un usuario con la dirección de correo ingresada." => BadRequest(message),
                _ => StatusCode(500, "Error inesperado: " + message)
            };
        }

        [HttpDelete("Delete{userId}")]
        public IActionResult Delete(int userId)
        {
            string message = _userService.Delete(userId);
            return message switch
            {
                "Usuario y restaurante borrados correctamente." => Ok(message),   
                "El usuario no existe." => NotFound(message),                     
                _ => StatusCode(500, "Error inesperado: " + message)              
            };
        }
    }
}
