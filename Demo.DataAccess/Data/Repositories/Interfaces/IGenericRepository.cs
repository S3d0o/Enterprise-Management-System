using Demo.DataAccess.Models.Shared;

namespace Demo.DataAccess.Data.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        int Add(TEntity entity);
        int Delete(TEntity entity);
        IEnumerable<TEntity> GetAll(bool withtracking = false);
        TEntity? GetById(int id);
        int Update(TEntity entity);
    }
}
