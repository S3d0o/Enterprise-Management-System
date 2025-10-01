using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Data.Repositories.Repositories;

namespace Demo.DataAccess.Data.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
        }

        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public void Dispose()
        {
            _dbContext.Dispose();// to release the resources
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges(); // return the number of affected rows
        }
    }
}
