namespace Demo.BusinessLogic.DTOS
{
    public class DepartmentDetailsDto
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; } // user id
        public int ModifiedBy { get; set; } // user id
        public DateOnly? CreatedAt { get; set; } // the date time of creation
        public DateOnly? ModifiedAt { get; set; } // the date time of modification
        public bool IsDeleted { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
