using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaLlega.Entities
{
    [Index(nameof(EmailAddress), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
        public Restaurant Restaurant { get; set; }
    }
}
