using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.Shared;

namespace Demo.DataAccess.Data.Repositories.Classes
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public IEnumerable<TEntity> GetAll(bool withtracking = false)
        {
            if (withtracking)
                return _dbContext.Set<TEntity>().ToList();
            return _dbContext.Set<TEntity>().AsNoTracking().ToList();
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
    }
}
