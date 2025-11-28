using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class UsersServiceResult
    {
        [Required]
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
