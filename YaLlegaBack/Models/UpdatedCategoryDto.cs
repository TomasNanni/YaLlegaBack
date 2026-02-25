using System.ComponentModel.DataAnnotations;
using YaLlega.Entities;

namespace YaLlegaBack.Models
{
    public class UpdatedCategoryDto
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<int> ProductIds { get; set; } = new List<int>();
    }
}
