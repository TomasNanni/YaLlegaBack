using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public AuthenticationController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthDto credentials)
        {
            if (credentials == null || string.IsNullOrWhiteSpace(credentials.EmailAddress) || string.IsNullOrWhiteSpace(credentials.Password))
            {
                return BadRequest("Debe proporcionar email y contraseña válidos.");
            }

            try
            {
                int? userId = _userService.Authenticate(credentials.EmailAddress, credentials.Password);

                if (userId is not null)
                {
                    var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

                    var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                    var claimsForToken = new List<Claim>
                    {
                        new Claim("sub", userId.ToString()),
                        new Claim("")
                    };

                    var jwtSecurityToken = new JwtSecurityToken(
                        _config["Authentication:Issuer"],
                        _config["Authentication:Audience"],
                        claimsForToken,
                        DateTime.UtcNow,
                        DateTime.UtcNow.AddHours(1),
                        signature
                    );

                    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    return Ok(tokenToReturn);
                }

                return Unauthorized();
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
        [HttpGet("validateOwner/{restaurantId}")]
        [Authorize]
        public IActionResult ValidateOwner(int restaurantId)
        {
            if (restaurantId <= 0)
            {
                return BadRequest("El id debe ser mayor a 0");
            }

            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized("Usuario no validado.");
                }

                int userId = int.Parse(userIdClaim);
                if (userId <= 0)
                {
                    return Unauthorized("Usuario inválido.");
                }

                if (userId == restaurantId)
                {
                    return Ok();
                }
                else
                {
                    return Forbid();
                }
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
