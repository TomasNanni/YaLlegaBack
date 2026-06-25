using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class NewUpdatedRestaurantDTO
    {
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        public string UrlLogoImage { get; set; }
        public string UrlBannerImage { get; set; }
        [Required]
        public List<string> OpenDays { get; set; }
        [Required]
        public TimeOnly OpeningTime { get; set; }
        [Required]
        public TimeOnly ClosingTime { get; set; }
        [Required]
        public string Contact { get; set; }
    }
}
