using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CommandLine;
using SamuraiConsoleApplication.Data;
using SamuraiConsoleApplication.Services;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SamuraiConsoleApplication
{
  class Program
  {
    static async Task Main(string[] args)
    {
      var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
      XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

      var options = ParseArguments(args);
      var configuration = LoadConfiguration();
      var services = ConfigureServices(configuration);
      var serviceProvider = services.BuildServiceProvider();

      await serviceProvider.GetService<ConsoleApplication>().Run(options);
    }

    private static Options ParseArguments(string[] args)
    {
      var result = Parser.Default.ParseArguments<Options>(args);
      if (result.Tag == ParserResultType.Parsed)
        return ((Parsed<Options>)result).Value;
      throw new InvalidCastException("Could not parse arguments");
    }

    private static IConfiguration LoadConfiguration()
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true);
      return builder.Build();
    }

    private static IServiceCollection ConfigureServices(IConfiguration configuration)
    {
      var services = new ServiceCollection();
      services.AddSingleton(configuration);
      services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
      services.AddMongo();
      services.AddScoped<ISamuraiRepository, SamuraiRepository>();
      services.AddTransient<ISamuraiService, SamuraiService>();

      services.AddTransient<ConsoleApplication>();
      return services;
    }
  }
}
