using System.Threading;
using System.Threading.Tasks;
using Shamyr.Urlik.Service.Dtos;
using StackExchange.Redis;

namespace Shamyr.Urlik.Service.Services
{
  public interface IRedisService
  {
    bool IsConnected { get; }

    Task<long> CountHitsAsync(CommandFlags commandFlags, CancellationToken cancellationToken);
    Task<string?> GetPathAsync(string path, CancellationToken cancellationToken);
    Task PushHitAsync(HitDto dto, CommandFlags commandFlags, CancellationToken cancellationToken);
    Task SetPathAsync(string path, string url, CommandFlags commandFlags, CancellationToken cancellationToken);
    Task TrimHitsAsync(int start, int stop, CommandFlags commandFlags, CancellationToken cancellationToken);
    Task UnsetPathAsync(string path, CommandFlags commandFlags, CancellationToken cancellationToken);
  }
}