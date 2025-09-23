using Demo.DataAccess.Models.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DataAccess.Data.Configurations
{
    internal class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(d => d.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(d => d.ModifiedAt).HasComputedColumnSql("GETDATE()");
        }
    }
}
