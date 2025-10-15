using System.ComponentModel.DataAnnotations;

namespace Demo.BusinessLogic.DTOS.RoleDTOS
{
    public class RoleCreateDto
    {
        [MinLength(3, ErrorMessage = "Name must contain at least 3 chars"), MaxLength(15)]
        public string Name { get; set; } = string.Empty;

        [MinLength(4,ErrorMessage ="Description must contain at least 4 chars"),MaxLength(30,ErrorMessage ="too many Chars only 30 are allowed")]
        public string Description { get; set; } = string.Empty;
    }
}
