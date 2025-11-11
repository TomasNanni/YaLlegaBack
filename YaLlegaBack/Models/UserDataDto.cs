using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YaLlega.Entities;

namespace YaLlegaBack.Models
{
    public class UserDataDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
