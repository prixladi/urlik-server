using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Repositories;
using Shamyr.Urlik.Database.Documents;

namespace Shamyr.Urlik.Service.Repositories
{
  public class UrlRepository: RepositoryBase<UrlDoc>
  {
    public UrlRepository(IDatabaseContext dbContext)
      : base(dbContext) { }

    public async Task<UrlDoc?> GetByPathAsync(string path, CancellationToken cancellationToken)
    {
      return await Query.SingleOrDefaultAsync(doc => doc.Part == path, cancellationToken);
    }
  }
}
