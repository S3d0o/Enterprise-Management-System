namespace Demo.BusinessLogic.DTOS
{
    public class DepartmentDto // GetAll Validation => Id,Code,Name,Description,DateOfCreation [Date part only]
    {
        public int DeptId { get; set; }
        public string DeptCode { get; set; } = string.Empty;
        public string DeptName { get; set; } = null!;
        public string? DeptDescription { get; set; } = string.Empty;
        public DateOnly DateOfCreation { get; set; }
    }
}
