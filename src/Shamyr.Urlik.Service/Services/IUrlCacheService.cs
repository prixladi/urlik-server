using System.Threading;
using System.Threading.Tasks;

namespace Shamyr.Urlik.Service.Services
{
  public interface IUrlCacheService
  {
    Task<string?> TryGetAsync(string path, CancellationToken cancellationToken);
    Task SetAsync(string path, string url, CancellationToken cancellationToken);
    Task UnsetAsync(string path, CancellationToken cancellationToken);
  }
}