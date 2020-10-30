using Shamyr.MongoDB.Configs;

namespace Shamyr.Urlik.Service.Configs
{
  public class DatabaseConfig: IDatabaseConfig
  {
    public string DatabaseUrl => throw new System.NotImplementedException();

    public string DatabaseName => throw new System.NotImplementedException();
  }
}
