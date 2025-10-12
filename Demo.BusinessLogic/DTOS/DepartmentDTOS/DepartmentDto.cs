using System.ComponentModel;

namespace Demo.BusinessLogic.DTOS.DepartmentDTOS
{
    public class DepartmentDto // GetAll Validation => Id,Code,Name,Description,DateOfCreation [Date part only]
    {
        [DisplayName("ID")]
        public int DeptId { get; set; }

        [DisplayName("Code")]
        public string DeptCode { get; set; } = string.Empty;

        [DisplayName("Name")]
        public string DeptName { get; set; } = null!;

        [DisplayName("Description")]
        public string? DeptDescription { get; set; } = string.Empty;

        [DisplayName("Date Of Creation")]
        public DateOnly DateOfCreation { get; set; }

        [DisplayName("Number of members")]
        public int MemberCount { get; set; }
    }
}
