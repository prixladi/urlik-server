using System;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Urlik.Service.Repositories;
using StackExchange.Redis;

namespace Shamyr.Urlik.Service.Services
{
  public class RedisService: IRedisService
  {
    private readonly IDatabase fDatabase;

    public RedisService(IRedisDatabaseRepository redisDatabaseRepository)
    {
      fDatabase = redisDatabaseRepository.GetDatabase();
    }

    public bool IsConnected => fDatabase.Multiplexer.IsConnected;

    public async Task SetAsync(string path, string url, CancellationToken cancellationToken)
    {
      await fDatabase.StringSetAsync(new RedisKey(path), new RedisValue(url), TimeSpan.FromMinutes(30));
    }

    public async Task<string?> GetAsync(string path, CancellationToken cancellationToken)
    {
      var value = await fDatabase.StringGetAsync(new RedisKey(path));
      return value;
    }

    public async Task UnsetAsync(string path, CancellationToken cancellationToken)
    {
      await fDatabase.KeyDeleteAsync(new RedisKey(path));
    }
  }
}
