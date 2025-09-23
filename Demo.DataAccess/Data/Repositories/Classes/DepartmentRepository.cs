using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories.Repositories;

namespace Demo.DataAccess.Data.Repositories.Classes
{
    // C# 12 feature  .Net 8 feature => Primar constructors for classes
    public class DepartmentRepository(AppDbContext _dbContext) : GenericRepository<Department>(_dbContext), IDepartmentRepository
    {

    }
}
