using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Extensions.Hosting;
using Shamyr.Urlik.Service.Configs;
using Shamyr.Urlik.Service.Services;
using StackExchange.Redis;

namespace Shamyr.Urlik.Service.HostedServices
{
  public class RedisTrimService: CronServiceBase
  {
    private readonly IRedisConfig fRedisConfig;

    public RedisTrimService(IServiceProvider serviceProvider, IRedisConfig redisConfig)
      : base(redisConfig.HitsTrimInterval, serviceProvider)
    {
      fRedisConfig = redisConfig;
    }

    protected override async Task ExecuteAsync(IServiceProvider provider, CancellationToken cancellationToken)
    {
      var redisService = provider.GetRequiredService<IRedisService>();

      var capacity = fRedisConfig.HitsCapacity;
      var size = await redisService.CountHitsAsync(CommandFlags.None, cancellationToken);
      if (size >= capacity)
        await redisService.TrimHitsAsync(0, capacity, CommandFlags.None, cancellationToken);
    }
  }
}
