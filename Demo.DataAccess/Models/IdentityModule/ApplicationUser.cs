using Microsoft.AspNetCore.Identity;

namespace Demo.DataAccess.Models.IdentityModule
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

    }
}
