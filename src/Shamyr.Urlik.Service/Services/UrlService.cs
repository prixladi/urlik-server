using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Urlik.Database.Documents;
using Shamyr.Urlik.Service.Dtos;

namespace Shamyr.Urlik.Service.Services
{
  public class UrlService: IUrlService
  {
    public Task<string?> TryGetAsync(string path, CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }

    public Task CreateAsync(UrlDoc doc, CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> UpdateAsync(ObjectId id, UpdateUrlDto dto, CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> DeleteAsync(ObjectId id, UpdateUrlDto dto, CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }
  }
}
