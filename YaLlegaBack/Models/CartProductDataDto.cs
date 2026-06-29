using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class CartProductDataDto : ProductDataDto
    {
        [Required]
        public int Amount { get; set; }
    }
}
