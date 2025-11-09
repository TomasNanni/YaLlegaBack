using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaLlega.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name{ get; set; }
        public string Description { get; set; }
        [Required]
        public string UrlImage { get; set; }
        [Required]
        public decimal BasePrice { get; set; }
        public int Discount { get; set; } = 0;
        [Required]
        public bool IsStandout = false;
        public TimeOnly ?HappyHourStart { get; set; }
        public TimeOnly ?HappyHourEnd { get; set; }
        public ICollection<Category> Categories{ get; set; } = new List<Category>();

        public int ?CartId { get; set; }

        [ForeignKey("CartId")]
        public Cart ?Cart { get; set; }
    }
}
