using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SamuraiConsoleApplication.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SamuraiConsoleApplication.Data
{
  public class SamuraiRepository : Repository<Samurai>, ISamuraiRepository
  {

    public SamuraiRepository(MongoDbContext context) : base(context)
    {
    }

    public async Task<List<Samurai>> CreateSamurai(IEnumerable<Samurai> samurai)
    {
      var addMany = await AddMany(samurai);
      return addMany.ToList();
    }

    public Task<Samurai> DeleteSamurai(string id)
    {
      throw new NotImplementedException();
    }
  }
}