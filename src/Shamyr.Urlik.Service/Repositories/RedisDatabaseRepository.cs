using System;
using Shamyr.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Shamyr.Urlik.Service.Repositories
{
  [Singleton]
  public class RedisDatabaseRepository: IRedisDatabaseRepository
  {
    private IDatabase? fDatabase;

    public IDatabase GetDatabase()
    {
      return fDatabase ?? throw new InvalidOperationException("Redis instance is not set.");
    }

    public void SetDatabase(IDatabase database)
    {
      fDatabase = database;
    }
  }
}
