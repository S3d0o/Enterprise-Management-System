using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.IdentityModule;
using Demo.DataAccess.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DataAccess.Data.Configurations
{
    internal class DepartmentConfig : BaseEntityConfigurations<Department>, IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            // 🔹 Primary key and identity seed
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);

            // 🔹 Basic properties
            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(d => d.Code)
                   .HasMaxLength(20);

            // 🔹 Relationship with Employees
            builder.HasMany(d => d.Employees)
                   .WithOne(e => e.Department)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);

            // 🔹 Audit relationships
            builder.HasOne(d => d.CreatedByUser)
     .WithMany()
     .HasForeignKey(d => d.CreatedById)
     .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ModifiedByUser)
                .WithMany()
                .HasForeignKey(d => d.ModifiedById)
                .OnDelete(DeleteBehavior.Restrict);


            // 🔹 Apply any shared configuration logic from BaseEntityConfigurations
            base.Configure(builder);
        }
    }
}
