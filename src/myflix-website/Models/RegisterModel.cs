using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace myflix_website.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username {  get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
