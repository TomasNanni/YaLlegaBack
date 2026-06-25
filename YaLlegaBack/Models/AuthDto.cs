using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class AuthDto
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
