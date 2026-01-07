using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YaLlega.Entities;

namespace YaLlegaBack.Models
{
    public class CartDataDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public ICollection<ProductDataDto> Products { get; set; } = new List<ProductDataDto>();
    }
}
