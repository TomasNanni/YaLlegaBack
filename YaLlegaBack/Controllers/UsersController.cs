using Microsoft.AspNetCore.Mvc;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;


namespace YaLlegaBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userRepository)
        {
            _userService = userRepository;
        }

        [HttpGet]
        public ActionResult<UserDataDto> GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("id/{id}")]
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

        [HttpGet("email/{email}")]
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

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
