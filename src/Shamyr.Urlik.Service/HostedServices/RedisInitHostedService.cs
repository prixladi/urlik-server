using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Shamyr.Threading;
using Shamyr.Urlik.Service.Configs;
using Shamyr.Urlik.Service.Repositories;
using StackExchange.Redis;

namespace Shamyr.Urlik.Service.HostedServices
{
  public class RedisInitHostedService: IHostedService
  {
    private readonly IRedisDatabaseRepository fDatabaseRepository;
    private readonly IRedisConfig fRedisConfig;
    private readonly AsyncLock fLock;

    private ConnectionMultiplexer? fMultiplexer;

    public RedisInitHostedService(IRedisDatabaseRepository databaseRepository, IRedisConfig redisConfig)
    {
      fDatabaseRepository = databaseRepository;
      fRedisConfig = redisConfig;
      fLock = new AsyncLock();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      fMultiplexer = await ConnectionMultiplexer.ConnectAsync(new ConfigurationOptions
      {
        EndPoints =
        {
          { fRedisConfig.Host, fRedisConfig.Port}
        }
      });
      fDatabaseRepository.SetDatabase(fMultiplexer.GetDatabase());
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
      using (await fLock.LockAsync(cancellationToken))
      {
        if (fMultiplexer == null)
          return;

        try
        {
          await fMultiplexer.CloseAsync(false);
          fMultiplexer.Dispose();
        }
        finally
        {
          fMultiplexer = null;
        }
      }
    }
  }
}
