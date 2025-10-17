using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.IdentityModule;
using Demo.DataAccess.Models.Shared;

namespace Demo.DataAccess.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        // Add these navigation properties if you want to show usernames too:
        public virtual ApplicationUser? CreatedByUser { get; set; }
        public virtual ApplicationUser? ModifiedByUser { get; set; }
    }
}
