using bluestone.guests.data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace bluestone.guests.core.Repositories
  {
  public class Repository<TEntity> : IRepository<TEntity> 
    where TEntity : class 
    {
    protected readonly DbContext Context;

    public Repository(DbContext context)
      {
      Context = context;
      }


    public async Task AddAsync(TEntity entity)
      {
      DbSet<TEntity> _set = Context.Set<TEntity>();

      await _set.AddAsync(entity);
      }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
      {
      await Context.Set<TEntity>().AddRangeAsync(entities);
      }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
      {
      return Context.Set<TEntity>().Where(predicate);
      }

    public List<TEntity> GetAll()
      {
      DbSet<TEntity> _set = Context.Set<TEntity>();

      return _set.ToList<TEntity>();
      }

    public async Task<List<TEntity>> GetAllAsync()
      {
      DbSet<TEntity> _set = Context.Set<TEntity>();

      return await _set.ToListAsync<TEntity>();
      }

    public ValueTask<TEntity> GetByIdAsync(Guid id)
      {
      DbSet<TEntity> _set = Context.Set<TEntity>();

      ValueTask<TEntity> _entity = _set.FindAsync(id);

      return _entity;
      }

    public void Remove(TEntity entity)
      {
      Context.Set<TEntity>().Remove(entity);
      }

    public void RemoveRange(IEnumerable<TEntity> entities)
      {
      Context.Set<TEntity>().RemoveRange(entities);
      }

    public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
      {
      return Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
      }

    public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
      {
      return Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
      }

    public async Task<int> Count()
      {
      return await Context.Set<TEntity>().CountAsync();
      }
    }
  }