using System.Collections.Generic;
using System.Threading.Tasks;
using SamuraiConsoleApplication.Models;

namespace SamuraiConsoleApplication.Data
{
  public interface ISamuraiRepository : IRepository<Samurai>
  {
    Task<List<Samurai>> CreateSamurai(IEnumerable<Samurai> samurai);
    Task<Samurai> DeleteSamurai(string id);
  }
}