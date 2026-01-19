using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaLlega.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(20 , MinimumLength = 1)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int RestaurantUserId { get; set; }
        [ForeignKey("RestaurantUserId")]
        public Restaurant Restaurant { get; set; }
        public ICollection<Product> Products{ get; set; } = new List<Product>();
    }
}
