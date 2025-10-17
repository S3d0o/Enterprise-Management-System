using Demo.DataAccess.Models.EmployeeModule;
using Microsoft.AspNetCore.Identity;

namespace Demo.DataAccess.Models.IdentityModule
{
    public class ApplicationUser : IdentityUser
    {
       public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        // employees owned by this user
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        // optional: track who created/modified employees
        public virtual ICollection<Employee> CreatedEmployees { get; set; } = new List<Employee>();
        public virtual ICollection<Employee> ModifiedEmployees { get; set; } = new List<Employee>();
   

    }
}
