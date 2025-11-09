using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace YaLlega1.Models
{
    public class AuthDto
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
