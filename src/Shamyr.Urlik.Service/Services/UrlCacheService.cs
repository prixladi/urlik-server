using System;
using System.Threading;
using System.Threading.Tasks;
using Shamyr.Logging;
using StackExchange.Redis;

namespace Shamyr.Urlik.Service.Services
{
  public class UrlCacheService: IUrlCacheService
  {
    private readonly ILogger fLogger;
    private readonly IRedisService fRedisService;

    public UrlCacheService(ILogger logger, IRedisService redisService)
    {
      fLogger = logger;
      fRedisService = redisService;
    }

    public async Task<string?> TryGetUrlAsync(string path, ILoggingContext context, CancellationToken cancellationToken)
    {
      return await ActionAsync((redis) => redis.GetPathAsync(path, cancellationToken), context);
    }

    public async Task SetUrlAsync(string path, string url, ILoggingContext context, CancellationToken cancellationToken)
    {
      await ActionAsync((redis) => redis.SetPathAsync(path, url, CommandFlags.None, cancellationToken), context);
    }

    public async Task SetUrlFireAndForgetAsync(string path, string url, ILoggingContext context, CancellationToken cancellationToken)
    {
      await ActionAsync((redis) => redis.SetPathAsync(path, url, CommandFlags.FireAndForget, cancellationToken), context);
    }

    public async Task UnsetUrlAsync(string path, ILoggingContext context, CancellationToken cancellationToken)
    {
      await ActionAsync((redis) => redis.UnsetPathAsync(path, CommandFlags.None, cancellationToken), context);
    }

    private async Task ActionAsync(Func<IRedisService, Task> func, ILoggingContext context)
    {
      try
      {
        if (fRedisService.IsConnected)
          await func(fRedisService);
        else
          fLogger.LogError(context, $"Cache service is not conected.");
      }
      catch (Exception ex)
      {
        fLogger.LogException(context, ex);
      }
    }

    private async Task<string?> ActionAsync(Func<IRedisService, Task<string?>> func, ILoggingContext context)
    {
      try
      {
        if (fRedisService.IsConnected)
          return await func(fRedisService);
        else
        {
          fLogger.LogError(context, $"Cache service is not conected.");
          return null;
        }
      }
      catch (Exception ex)
      {
        fLogger.LogException(context, ex);
        return null;
      }
    }
  }
}
