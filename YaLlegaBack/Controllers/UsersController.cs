using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
            try
            {
                IEnumerable<UserDataDto> users = _userService.GetAll();
                if (users?.Any() != true)
                {
                    return NoContent();
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("GetOneByid/{id}")]
        [Authorize]
        public IActionResult GetOneById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID ingresado debe ser distinto de 0");
            }

            try
            {
                GetUserByIdDto? user = _userService.GetById(id);

                if (user is null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("GetOneByEmail/{email}")]
        public IActionResult GetOneByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Debe ingresar una dirección de email válida.");
            }

            try
            {
                GetUserByIdDto? user = _userService.GetByEmail(email);

                if (user is null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

 
        [HttpPost("Create")]
        [AllowAnonymous]
        public IActionResult Create([FromBody] CreateUserRequest dto)
        {
            if (dto == null || dto.User == null || dto.Restaurant == null)
            {
                return BadRequest("Debe proporcionar datos de usuario y restaurante válidos.");
            }

            try
            {
                int? newUserId = _userService.Create(dto.User, dto.Restaurant);
                if (newUserId == null || newUserId <= 0)
                {
                    return BadRequest("Ya existe un usuario con esos datos.");
                }

                GetUserByIdDto userDto = _userService.GetById((int)newUserId);
                GetRestaurantByIdDto restaurant = _userService.GetRestaurantDto((int)newUserId);
                var response = new
                {
                    Mensaje = "Usuario y restaurante creados correctamente.",
                    Usuario = userDto,
                    Restaurante = restaurant
                };
                return Created("Usuario Creado: ", response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("Update")]
        [Authorize]
        public IActionResult UpdateUser(UpdateUserDto updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest("Debe proporcionar datos válidos de usuario.");
            }

            try
            {
                int userToUpdateId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userToUpdateId <= 0)
                {
                    return Unauthorized("Usuario no validado.");
                }
                ServiceResult result = _userService.Update(updatedUser, userToUpdateId);
                return StatusCode(result.StatusCode, result.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("Delete")]
        [Authorize]
        public IActionResult Delete()
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId <= 0)
                {
                    return Unauthorized("Usuario no validado.");
                }
                ServiceResult message = _userService.Delete(userId);
                return StatusCode(message.StatusCode, message.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
