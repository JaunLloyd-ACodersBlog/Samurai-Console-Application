using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SamuraiConsoleApplication.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SamuraiConsoleApplication.Data
{
  public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntityAudit
  {
    public IMongoCollection<TEntity> Collection { get; set; }

    public Repository(MongoDbContext context)
    {
      Collection = context.Collection<TEntity>();
    }

    public async Task<TEntity> Get(string id)
    {
      return await Collection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> GetAll()
    {
      return await Collection.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
    {
      return await Collection.Find(predicate).ToListAsync();
    }

    public async Task Add(TEntity entity)
    {
      entity.CreatedDate = DateTime.Now;
      await Collection.InsertOneAsync(entity);
    }
    public async Task<IEnumerable<TEntity>> AddMany(IEnumerable<TEntity> entities)
    {
      var baseEntityAudits = entities.ToList();
      var entityAudits = baseEntityAudits.ToList().Select(x =>
      {
        x.CreatedDate = DateTime.Now;
        return x;
      }).ToList();
      await Collection.InsertManyAsync(entityAudits);
      return entityAudits;
    }

    public async Task Update(TEntity entity)
    {
      await Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = true });
    }

    public async Task<bool> Remove(string id)
    {
      var deleteResult = await Collection.DeleteOneAsync(x => x.Id == ObjectId.Parse(id));
      return deleteResult.IsAcknowledged;
    }
  }
}