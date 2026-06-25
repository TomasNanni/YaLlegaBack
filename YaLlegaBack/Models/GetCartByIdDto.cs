using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class GetCartByIdDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public ICollection<ProductDataDto> Products { get; set; } = new List<ProductDataDto>();
    }
}
