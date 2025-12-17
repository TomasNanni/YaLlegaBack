using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class NewUpdatedProductDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string UrlImage { get; set; }
        [Required]
        public decimal BasePrice { get; set; }
        [Required]
        public int Discount { get; set; } = 0;
        [Required]
        public bool IsStandout = false;
        public TimeOnly? HappyHourStart { get; set; }
        public TimeOnly? HappyHourEnd { get; set; }
    }
}
