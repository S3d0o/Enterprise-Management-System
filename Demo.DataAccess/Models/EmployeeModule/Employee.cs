using Demo.DataAccess.Models.Shared;

namespace Demo.DataAccess.Models.EmployeeModule
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Address { get; set; } 
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public Gender Gender { get; set; }

    }
}
