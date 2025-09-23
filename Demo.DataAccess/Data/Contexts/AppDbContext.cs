using Demo.DataAccess.Models.EmployeeModule;

namespace Demo.DataAccess.Data.Contexts
{
    // C# 12 feature  .Net 8 feature => Primar constructors for classes
    public class AppDbContext(DbContextOptions _options) : DbContext(_options)
    {

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("");
        //}
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
