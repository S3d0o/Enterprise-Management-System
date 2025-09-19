namespace Demo.DataAccess.Data.Repositories.Repositories
{
    public interface IDepartmentRepository
    {
        int Add(Department department);
        int Delete(Department department);
        IEnumerable<Department> GetAll(bool withtracking = false);
        Department? GetDepartmentById(int id);
        int Update(Department department);
    }
}