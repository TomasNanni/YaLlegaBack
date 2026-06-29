using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class UpdateUserDto
    {
        [StringLength(20)]
        public string? FirstName { get; set; }
        [StringLength(20)]
        public string? LastName { get; set; }
        [EmailAddress]
        [StringLength(30)]
        public string? EmailAddress { get; set; }
    }
}
