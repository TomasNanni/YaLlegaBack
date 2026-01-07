using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaLlegaBack.Models
{
    public class ProductDataDto
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
