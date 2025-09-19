using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Demo.DataAccess.Data.Configurations
{
    internal class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d=>d.Id).UseIdentityColumn(10,10);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(20);
            builder.Property(d => d.Code).HasMaxLength(20);
            builder.Property(d=>d.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(d=>d.ModifiedAt).HasComputedColumnSql("GETDATE()");

        }
    }
}
