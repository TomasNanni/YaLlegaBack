using System.ComponentModel.DataAnnotations;

namespace YaLlegaBack.Models
{
    public class GetUserByIdDto
    {
        [Required]
        public int Id { get; set; }
        [StringLength(20)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(20)]
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [StringLength(30)]
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string RestaurantName { get; set; }
        [Required]
        public string UrlLogoImage { get; set; }
        public string UrlBannerImage { get; set; }
        [Required]
        public string OpenDays { get; set; }
        [Required]
        public TimeOnly OpeningTime { get; set; }
        [Required]
        public TimeOnly ClosingTime { get; set; }
        [Required]
        public string Contact { get; set; }
    }
}
