using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YaLlega.Entities;

namespace YaLlegaBack.Models
{
    public class CategoryDataDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductDataDto> Products { get; set; } = new List<ProductDataDto>();
    }
}
