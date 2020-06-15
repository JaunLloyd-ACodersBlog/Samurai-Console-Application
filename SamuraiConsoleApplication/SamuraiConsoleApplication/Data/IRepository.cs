using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SamuraiConsoleApplication.Data
{
  public interface IRepository<TEntity>
  {
    Task<TEntity> Get(string id);
    Task<List<TEntity>> GetAll();
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
    Task Add(TEntity entity);
    Task<IEnumerable<TEntity>> AddMany(IEnumerable<TEntity> entities);
    Task Update(TEntity entity);
    Task<bool> Remove(string id);
  }
}