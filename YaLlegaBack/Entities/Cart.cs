using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YaLlegaBack.Models;

namespace YaLlega.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public ICollection<CartProductOrder> Products{ get; set; } = new List<CartProductOrder>();
    }
}
