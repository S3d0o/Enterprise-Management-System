using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.EmployeeModule;

namespace Demo.DataAccess.Data.Repositories.Classes
{
    public class EmployeeRepository(AppDbContext _dbContext) :GenericRepository<Employee>(_dbContext), IEmployeeRepository
    {
    }
}
