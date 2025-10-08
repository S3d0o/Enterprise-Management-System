namespace Demo.Presentaion.ViewModels.Department
{
    public class DepartmentViewModel
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}
