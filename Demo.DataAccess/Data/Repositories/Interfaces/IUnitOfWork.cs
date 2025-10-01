using Demo.DataAccess.Data.Repositories.Repositories;
using System.Diagnostics.Contracts;

namespace Demo.DataAccess.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        // Save changes
        public int SaveChanges(); // return the number of affected rows
    }
}
