using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace myflix_website.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username {  get; set; }

        [Required]
        [Length(6,100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
