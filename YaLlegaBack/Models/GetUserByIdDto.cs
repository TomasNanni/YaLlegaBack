using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class GetUserByIdDto
    {
        [Required]
        public int Id { get; set; }
        [StringLength(20)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(20)]
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [StringLength(30)]
        [Required]
        public string EmailAddress { get; set; }
    }
}
