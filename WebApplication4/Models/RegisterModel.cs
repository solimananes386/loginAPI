using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class RegisterModel
    {

        [Required , MaxLength(50)]
        public string FirstName { get; set; }
        [Required , MaxLength(50)]
        public string LastName { get; set; }
        [Required , MaxLength(50)]
        public string UserName { get; set; }
        [Required , EmailAddress]
        public string Email { get; set; }
        [Required , MaxLength(50)]
        public string Password { get; set; }
    }
}
