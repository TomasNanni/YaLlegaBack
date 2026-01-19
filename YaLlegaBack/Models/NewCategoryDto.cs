using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YaLlega.Entities;

namespace YaLlegaBack.Models
{
    public class NewCategoryDto
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int RestaurantUserId { get; set; }
        public List<int> productsId { get; set; }
    }
}
