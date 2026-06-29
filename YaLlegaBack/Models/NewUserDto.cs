using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class NewUserDto
    {
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(30)]
        public string EmailAddress { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        [PasswordPropertyText]
        [Compare("Password")]
        public string SecondPassword{ get; set; }
    }
}