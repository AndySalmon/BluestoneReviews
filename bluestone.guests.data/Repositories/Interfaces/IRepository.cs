using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace bluestone.guests.data.Repositories.Interfaces
  {
  public interface IRepository<TEntity>
  where TEntity : class
    {
    ValueTask<TEntity> GetByIdAsync(Guid id);
    
    List<TEntity> GetAll();
    Task<List<TEntity>> GetAllAsync();
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    Task<int> Count();

    Task AddAsync(TEntity entity);

    public Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);
    }
  }