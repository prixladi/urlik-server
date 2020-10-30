using Shamyr.MongoDB.Configs;

namespace Shamyr.Urlik.Service.Configs
{
  public class DatabaseConfig: IDatabaseConfig
  {
    public string DatabaseUrl => EnvVariable.Get(EnvVariables._MongoUrl);
    public string DatabaseName => EnvVariable.Get(EnvVariables._MongoDatabaseName);
  }
}
