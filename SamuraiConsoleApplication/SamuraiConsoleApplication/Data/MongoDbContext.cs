using MongoDB.Bson;
using MongoDB.Driver;

namespace SamuraiConsoleApplication.Data
{
  public class MongoDbContext
  {
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoClient client, string database)
    {
      _database = client.GetDatabase(database);
    }

    public IMongoCollection<T> Collection<T>(string collectionName = null)
    {
      var name = string.IsNullOrEmpty(collectionName) ? typeof(T).Name : collectionName;
      return _database.GetCollection<T>(name);
    }
  }
}
