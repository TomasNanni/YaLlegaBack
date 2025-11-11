using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YaLlega.Entities;

namespace YaLlega.Models
{
    public class NewUserDataDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAdress { get; set; }

        public string Password { get; set; }
        [Compare("Password")]
        public string SecondPassword{ get; set; }
        public Restaurant Restaurant { get; set; }
    }
}