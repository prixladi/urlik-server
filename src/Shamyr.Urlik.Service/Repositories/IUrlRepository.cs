using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.MongoDB.Repositories;
using Shamyr.Urlik.Database.Documents;
using Shamyr.Urlik.Service.Dtos;

namespace Shamyr.Urlik.Service.Repositories
{
  public interface IUrlRepository: IRepositoryBase<UrlDoc>
  {
    Task<bool> ExistByPathAsync(string path, CancellationToken cancellationToken);
    Task<UrlDoc?> GetByPathAsync(string path, CancellationToken cancellationToken);
    Task<List<UrlDoc>> GetByFilterAsync(FilterDto dto, CancellationToken cancellationToken);
    Task<int> CountByFilterAsync(FilterDto dto, CancellationToken cancellationToken);
    Task<UrlDoc?> RemoveAsync(ObjectId id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ObjectId id, UpdateUrlDto dto, CancellationToken cancellationToken);
  }
}