using System.Collections.Generic;
using System.Threading.Tasks;
using SamuraiConsoleApplication.Data;
using SamuraiConsoleApplication.Models;

namespace SamuraiConsoleApplication.Services
{
  public class SamuraiService : ISamuraiService
  {
    private readonly ISamuraiRepository _samuraiRepository;

    public SamuraiService(ISamuraiRepository samuraiRepository)
    {
      _samuraiRepository = samuraiRepository;
    }

    public async Task CreateABunchOfSamurai()
    {
      var samurai = CreateSamurai(10);
      await _samuraiRepository.CreateSamurai(samurai);
    }

    private IEnumerable<Samurai> CreateSamurai(int howMany = 1)
    {
      var samurai = new List<Samurai>();
      for (var i = 0; i < howMany; i++)
      {
        samurai.Add(new Samurai
        {
          Name = Faker.Name.First(),
          BowProficiency = Faker.RandomNumber.Next(10),
          SwordProficiency = Faker.RandomNumber.Next(10),
          HorseRidingProficiency = Faker.RandomNumber.Next(10),
          HorseName = Faker.Name.First()
        });
      }

      return samurai;
    }
  }
}
