using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class GetCategoryById
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductDataDto> Products { get; set; } = new List<ProductDataDto>();
    }
}
