using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaLlega.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        [Required]
        public ICollection<Product> Products{ get; set; } = new List<Product>();
        [Required]
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        [Required]
        public Restaurant Restaurant { get; set; }
    }
}
