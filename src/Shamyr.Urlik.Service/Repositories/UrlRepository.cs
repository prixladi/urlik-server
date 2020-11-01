using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shamyr.Extensions.DependencyInjection;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Repositories;
using Shamyr.Urlik.Database.Documents;
using Shamyr.Urlik.Service.Dtos;

namespace Shamyr.Urlik.Service.Repositories
{
  [Singleton]
  public class UrlRepository: RepositoryBase<UrlDoc>, IUrlRepository
  {
    public UrlRepository(IDatabaseContext dbContext)
      : base(dbContext) { }

    public async Task<bool> ExistByPathAsync(string path, CancellationToken cancellationToken)
    {
      return await Query.AnyAsync(doc => doc.Path == path, cancellationToken);
    }

    public async Task<List<UrlDoc>> GetByFilterAsync(FilterDto dto, CancellationToken cancellationToken)
    {
      return await Query
        .Where(doc => doc.UserId == dto.UserId)
        .Skip(dto.Skip)
        .Take(dto.Take)
        .ToListAsync(cancellationToken);
    }

    public async Task<int> CountByFilterAsync(FilterDto dto, CancellationToken cancellationToken)
    {
      return await Query
        .Where(doc => doc.UserId == dto.UserId)
        .CountAsync(cancellationToken);
    }

    public async Task<UrlDoc?> GetByPathAsync(string path, CancellationToken cancellationToken)
    {
      return await Query.SingleOrDefaultAsync(doc => doc.Path == path, cancellationToken);
    }

    public async Task<bool> UpdateAsync(ObjectId id, UpdateUrlDto dto, CancellationToken cancellationToken)
    {
      var update = Builders<UrlDoc>.Update
        .Set(doc => doc.Path, dto.Path)
        .Set(doc => doc.Url, dto.Url);

      var result = await UpdateAsync(id, update, cancellationToken);
      return result.MatchedCount > 0;
    }

    public async Task<UrlDoc?> RemoveAsync(ObjectId id, CancellationToken cancellationToken)
    {
      return await Collection.FindOneAndDeleteAsync(doc => doc.Id == id, null, cancellationToken);
    }
  }
}
