using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.EmployeeModule;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess.Data.Repositories.Classes
{
    public class EmployeeRepository(AppDbContext _dbContext) :GenericRepository<Employee>(_dbContext), IEmployeeRepository
    {
        public Employee? GetById(int id)
        {
            return _dbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.CreatedByUser)
                .Include(e => e.ModifiedByUser)
                .FirstOrDefault(e => e.Id == id);
        }
    }
}
