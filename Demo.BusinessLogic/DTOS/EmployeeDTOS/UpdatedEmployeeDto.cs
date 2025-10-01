using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace Demo.BusinessLogic.DTOS.EmployeeDTOS
{
    public class UpdatedEmployeeDto
    {
        public int id { get; set; } // efcore check if the entity already exists
        [Required(ErrorMessage = "Name Can't Be Null")]
        [MaxLength(50, ErrorMessage = "Max length should be 50 character")]
        [MinLength(3, ErrorMessage = "Min length should be 3 characters")]
        public string Name { get; set; } = null!;

        [Range(22, 35)]
        public int? Age { get; set; }
        [RegularExpression(@"^\d{1,5}-[A-Za-z]+(?:\s[A-Za-z]+)*-[A-Za-z]+(?:\s[A-Za-z]+)*-[A-Za-z]+$",
     ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; } // Foriegn Key
    }
}
