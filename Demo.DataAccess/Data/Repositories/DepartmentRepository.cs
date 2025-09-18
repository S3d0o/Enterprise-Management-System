using Demo.DataAccess.Data.Contexts;

namespace Demo.DataAccess.Data.Repositories
{
    // C# 12 feature  .Net 8 feature => Primar constructors for classes
    public class DepartmentRepository(AppDbContext _dbContext) : IDepartmentRepository
    {
        // Controller[PL] => Services[BLL] => Repositories[DAL] => DbContext[DAL] => Database

        // CRUD Operations
        // GET ALL
        public IEnumerable<Department> GetAll(bool withtracking = false)
        {
            if (withtracking)
                return _dbContext.Departments.ToList();
            return _dbContext.Departments.AsNoTracking().ToList();
        }

        // GET BY ID
        public Department? GetDepartmentById(int id)
        {
            var result = _dbContext.Departments.Find(id); // the connection will be opened and closed automatically , CLR will manage it
            if (result == null)
            {
                Console.WriteLine("the Department is not found");
            }
            return result;
        }

        //ADD
        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges(); // return the number of affected rows
        }

        //UPDATE
        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges(); // return the number of affected rows
        }

        //DELETE
        public int Delete(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges(); // return the number of affected rows

        }

    }
}
