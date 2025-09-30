using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.Shared;

namespace Demo.DataAccess.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
