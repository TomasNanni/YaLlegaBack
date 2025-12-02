using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YaLlega.Entities;

namespace YaLlega.Models
{
    public class NewUpdatedUserDto
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