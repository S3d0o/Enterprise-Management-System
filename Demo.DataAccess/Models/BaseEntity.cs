namespace Demo.DataAccess.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; } // user id
        public int ModifiedBy { get; set; } // user id
        public DateTime? CreatedAt { get; set; } // the date time of creation
        public DateTime? ModifiedAt { get; set; } // the date time of modification
        public bool IsDeleted { get; set; }
    }
}
