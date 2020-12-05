using System;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Text.Json;
using Shamyr.Urlik.Service.Dtos;
using Shamyr.Urlik.Service.Repositories;
using StackExchange.Redis;

namespace Shamyr.Urlik.Service.Services
{
  public class RedisService: IRedisService
  {
    private const string _HitsQueueName = "hits";

    private readonly IDatabase fDatabase;

    public RedisService(IRedisDatabaseRepository redisDatabaseRepository)
    {
      fDatabase = redisDatabaseRepository.GetDatabase();
    }

    public bool IsConnected => fDatabase.Multiplexer.IsConnected;

    public async Task SetPathAsync(string path, string url, CommandFlags commandFlags, CancellationToken cancellationToken)
    {
      await fDatabase.StringSetAsync(path, url, expiry: TimeSpan.FromMinutes(30), flags: commandFlags);
    }

    public async Task<string?> GetPathAsync(string path, CancellationToken cancellationToken)
    {
      return await fDatabase.StringGetAsync(path);
    }

    public async Task UnsetPathAsync(string path, CommandFlags commandFlags, CancellationToken cancellationToken)
    {
      await fDatabase.KeyDeleteAsync(path, flags: commandFlags);
    }

    public async Task PushHitAsync(HitDto dto, CommandFlags commandFlags, CancellationToken cancellationToken)
    {
      var content = await JsonConvert.SerializeAsync(dto, JsonConvert.CammelCaseOptions, cancellationToken);
      await fDatabase.ListLeftPushAsync(_HitsQueueName, new RedisValue[] { content }, flags: commandFlags);
    }

    public async Task TrimHitsAsync(int start, int stop, CommandFlags commandFlags, CancellationToken cancellationToken)
    {
      await fDatabase.ListTrimAsync(_HitsQueueName, start, stop, commandFlags);
    }

    public async Task<long> CountHitsAsync(CommandFlags commandFlags, CancellationToken cancellationToken)
    {
      return await fDatabase.ListLengthAsync(_HitsQueueName, commandFlags);
    }
  }
}
