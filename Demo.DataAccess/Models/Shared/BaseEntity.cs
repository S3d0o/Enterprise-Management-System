using Demo.DataAccess.Models.IdentityModule;

namespace Demo.DataAccess.Models.Shared
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public string? CreatedById { get; set; }
        public virtual ApplicationUser? CreatedByUser { get; set; }

        public string? ModifiedById { get; set; }
        public virtual ApplicationUser? ModifiedByUser { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
