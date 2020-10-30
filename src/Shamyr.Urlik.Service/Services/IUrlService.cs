using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Urlik.Database.Documents;
using Shamyr.Urlik.Service.Dtos;

namespace Shamyr.Urlik.Service.Services
{
  public interface IUrlService
  {
    Task CreateAsync(UrlDoc doc, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(ObjectId id, UpdateUrlDto dto, CancellationToken cancellationToken);
    Task<string?> TryGetAsync(string path, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ObjectId id, UpdateUrlDto dto, CancellationToken cancellationToken);
  }
}