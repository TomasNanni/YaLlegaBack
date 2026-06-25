using YaLlega.Entities;

namespace YaLlegaBack.Models
{
    public class UserDataDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
