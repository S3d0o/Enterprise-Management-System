using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.Shared;
using System.Linq.Expressions;

namespace Demo.DataAccess.Data.Repositories.Classes
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public IEnumerable<TEntity> GetAll(bool withtracking = false)
        {
            if (withtracking)
                return _dbContext.Set<TEntity>().Where(e => e.IsDeleted == false).ToList();
            return _dbContext.Set<TEntity>().Where(e => e.IsDeleted == false).AsNoTracking().ToList();
        }
        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
           return _dbContext.Set<TEntity>().Where(e => e.IsDeleted == false)
                .Select(selector).ToList();
        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>> predicate)
        {
           return _dbContext.Set<TEntity>().Where(predicate).Where(Entity=> Entity.IsDeleted == false).ToList();
        }
        // GET BY ID
        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id); // the connection will be opened and closed automatically , CLR will manage it
        //ADD
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges(); // return the number of affected rows
        }
        //UPDATE
        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges(); // return the number of affected rows
        }
        //DELETE
        public int Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges(); // return the number of affected rows

        }
        public IEnumerable<TEntity> GetIEnumerable()
        {
            return _dbContext.Set<TEntity>();
        }
        public IQueryable<TEntity> GetIQuerable()
        {
            return _dbContext.Set<TEntity>();
        }

    }
}
