using System.Threading;
using System.Threading.Tasks;
using Shamyr.Logging;

namespace Shamyr.Urlik.Service.Services
{
  public interface IUrlCacheService
  {
    Task<string?> TryGetUrlAsync(string path, ILoggingContext context, CancellationToken cancellationToken);
    Task SetUrlAsync(string path, string url, ILoggingContext context, CancellationToken cancellationToken);
    Task UnsetUrlAsync(string path, ILoggingContext context, CancellationToken cancellationToken);
    Task SetUrlFireAndForgetAsync(string path, string url, ILoggingContext context, CancellationToken cancellationToken);
  }
}