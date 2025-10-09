using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DataAccess.Data.Configurations
{
    internal class EmployeeConfig : BaseEntityConfigurations<Employee>, IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(E => E.Name).HasColumnType("varchar(50)");
            builder.Property(E => E.Address).HasColumnType("varchar(100)");
            builder.Property(E=>E.Salary).HasColumnType("decimal(10,2)");
            builder.Property(E => E.Gender).HasConversion(
                (empGender) => empGender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender,true));

            builder.Property(E=>E.EmployeeType).HasConversion<string>();

            base.Configure(builder);
        }
    }
}
