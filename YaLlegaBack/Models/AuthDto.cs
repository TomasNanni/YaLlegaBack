using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace YaLlega1.Models
{
    public class AuthDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
