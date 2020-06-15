using System.Threading.Tasks;
using SamuraiConsoleApplication.Services;

namespace SamuraiConsoleApplication
{
  public class ConsoleApplication
  {
    private readonly ISamuraiService _samuraiService;

    public ConsoleApplication(ISamuraiService samuraiService)
    {
      _samuraiService = samuraiService;
    }
    public async Task Run(Options options)
    {
      if (options.Samurai && options.Create)
        await _samuraiService.CreateABunchOfSamurai();

    }
  }
}
