using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class ServiceResult
    {
        [Required]
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
