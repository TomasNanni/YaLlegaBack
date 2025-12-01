using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YaLlega.Entities;
using YaLlega1.Models;
using YaLlegaBack.Interfaces;

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
            User? user = _userService.Authenticate(credentials.EmailAddress, credentials.Password);

            if (user is not null)
            {
                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

                var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
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
    }
}
