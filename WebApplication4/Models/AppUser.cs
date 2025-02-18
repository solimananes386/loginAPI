using Microsoft.AspNetCore.Identity;

namespace WebApplication4.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
