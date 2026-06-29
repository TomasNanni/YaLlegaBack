using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class UpdateUserDto
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
    }
}
