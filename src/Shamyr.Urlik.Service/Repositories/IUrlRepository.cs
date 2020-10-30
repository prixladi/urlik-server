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
    Task<UrlDoc?> GetByPathAsync(string path, CancellationToken cancellationToken);
    Task<List<UrlDoc>> GetByUserIdAsync(ObjectId userId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ObjectId id, UpdateUrlDto dto, CancellationToken cancellationToken);
  }
}