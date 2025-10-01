using Demo.DataAccess.Models.Shared;
using System.Linq.Expressions;

namespace Demo.DataAccess.Data.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetAll(bool withtracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity,TResult>> selector);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>> predicate);
        TEntity? GetById(int id);
        void Update(TEntity entity);
        public IEnumerable<TEntity> GetIEnumerable();
        public IQueryable<TEntity> GetIQuerable();
    }
}
