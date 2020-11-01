using StackExchange.Redis;

namespace Shamyr.Urlik.Service.Repositories
{
  public interface IRedisDatabaseRepository
  {
    IDatabase GetDatabase();
    void SetDatabase(IDatabase database);
  }
}