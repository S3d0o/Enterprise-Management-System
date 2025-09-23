namespace Demo.BusinessLogic.DTOS.DepartmentDTOS
{
    public class UpdatedDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateOnly CreatedAt { get; set; } // i think this should has a default value of current date not to make the user provide it, i think it will be readonly

    }
}
