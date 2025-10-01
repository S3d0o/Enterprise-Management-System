using Demo.DataAccess.Models.Shared;
using System.Linq.Expressions;

namespace Demo.DataAccess.Data.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        int Add(TEntity entity);
        int Delete(TEntity entity);
        IEnumerable<TEntity> GetAll(bool withtracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity,TResult>> selector);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>> predicate);
        TEntity? GetById(int id);
        int Update(TEntity entity);
        public IEnumerable<TEntity> GetIEnumerable();
        public IQueryable<TEntity> GetIQuerable();
    }
}
