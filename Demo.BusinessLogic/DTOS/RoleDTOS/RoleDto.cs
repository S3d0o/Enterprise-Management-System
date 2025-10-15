using System.ComponentModel.DataAnnotations;

namespace Demo.BusinessLogic.DTOS.RoleDTOS
{
    public class RoleDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Display(Name="Users Count")]
        public int UserCount { get; set; }
    }
}
