using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaLlega.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name{ get; set; }
        public string Description { get; set; }
        [Required]
        public string UrlImage { get; set; }
        [Required]
        [Range(1,100000)]
        public decimal BasePrice { get; set; }
        [Required]
        public int Discount { get; set; } = 0;
        [Required]
        public bool IsStandout { get; set; } = false;
        public TimeOnly? HappyHourStart { get; set; }
        public TimeOnly? HappyHourEnd { get; set; }
        public ICollection<Category> Categories{ get; set; } = new List<Category>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
