using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Logging;
using Shamyr.Urlik.Database.Documents;
using Shamyr.Urlik.Service.Dtos;

namespace Shamyr.Urlik.Service.Services
{
  public interface IUrlService
  {
    Task CreateAsync(UrlDoc doc, ILoggingContext context, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(ObjectId id, ILoggingContext context, CancellationToken cancellationToken);
    Task<string?> TryGetAsync(string path, ILoggingContext context, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ObjectId id, UpdateUrlDto dto, UrlDoc oldDoc, ILoggingContext context, CancellationToken cancellationToken);
  }
}