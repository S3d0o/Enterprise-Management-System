using Demo.DataAccess.Data.Repositories;

namespace Demo.BusinessLogic.Services
{
    internal class DepartmentService(IDepartmentRepository repo) // Dependency Injection, Primar constructors for classes
    {
        public void Test()
        {
            //DepartmentRepository repo = new DepartmentRepository(new DataAccess.Data.Contexts.AppDbContext); wrong way i should allow the DI container to create the instance
        }

    }
}
