using System.Threading;
using System.Threading.Tasks;

namespace Shamyr.Urlik.Service.Services
{
  public class UrlCacheService: IUrlCacheService
  {
    public Task<string?> TryGetAsync(string path, CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }

    public Task SetAsync(string path, string url, CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }

    public Task UnsetAsync(string path, CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }
  }
}
