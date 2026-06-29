using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class GetCartByIdDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public ICollection<CartProductDataDto> Products { get; set; } = new List<CartProductDataDto>();
    }
}
