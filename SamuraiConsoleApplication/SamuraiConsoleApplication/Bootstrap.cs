using System.Reflection;
using SamuraiConsoleApplication.Data;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace SamuraiConsoleApplication
{
  public static class Bootstrap
  {
    private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public static void AddMongo(this IServiceCollection services)
    {
      services.AddSingleton<IMongoClient>(x =>
      {
        var configuration = x.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("Mongo");

        var connectionUrl = new MongoUrl(connectionString);
        var clientSettings = MongoClientSettings.FromUrl(connectionUrl);
        clientSettings.ClusterConfigurator = builder =>
        {
          builder.Subscribe<CommandStartedEvent>(e => { log.Info($"{e.Command} - {e.Command.ToJson()}"); });
        };

        return new MongoClient(clientSettings);
      });
      services.AddScoped(x =>
      {
        var configuration = x.GetRequiredService<IConfiguration>();
        var database = configuration.GetValue<string>("MongoDatabase");
        var client = x.GetRequiredService<IMongoClient>();
        return new MongoDbContext(client, database);
      });
    }
  }
}
