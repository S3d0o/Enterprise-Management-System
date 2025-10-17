using System.ComponentModel.DataAnnotations;

namespace Demo.BusinessLogic.DTOS.DepartmentDTOS
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage ="Name is required !!")] // this is just an extra validation on top of the entity configuration
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateOnly CreatedAt { get; set; } // i think this should has a default value of current date not to make the user provide it
        public string? CreatedById { get; set; }

    }
}
