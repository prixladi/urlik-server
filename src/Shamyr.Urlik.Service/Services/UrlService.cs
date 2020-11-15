using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Logging;
using Shamyr.Urlik.Database.Documents;
using Shamyr.Urlik.Service.Dtos;
using Shamyr.Urlik.Service.Repositories;

namespace Shamyr.Urlik.Service.Services
{
  public class UrlService: IUrlService
  {
    private readonly IUrlRepository fUrlRepository;
    private readonly IUrlCacheService fUrlCacheService;

    public UrlService(IUrlRepository urlRepository, IUrlCacheService urlCacheService)
    {
      fUrlRepository = urlRepository;
      fUrlCacheService = urlCacheService;
    }

    public async Task<string?> TryGetAsync(string path, ILoggingContext context, CancellationToken cancellationToken)
    {
      var url = await fUrlCacheService.TryGetUrlAsync(path, context, cancellationToken);
      if (url == null)
      {
        var doc = await fUrlRepository.GetByPathAsync(path, cancellationToken);
        if (doc == null)
          return null;

        url = doc.Url;
        await fUrlCacheService.SetUrlFireAndForgetAsync(path, url, context, cancellationToken);
      }

      return url;
    }

    public async Task CreateAsync(UrlDoc doc, ILoggingContext context, CancellationToken cancellationToken)
    {
      await fUrlRepository.InsertAsync(doc, cancellationToken);
      await fUrlCacheService.SetUrlAsync(doc.Path, doc.Url, context, cancellationToken);
    }

    public async Task<bool> UpdateAsync(ObjectId id, UpdateUrlDto dto, UrlDoc oldDoc, ILoggingContext context, CancellationToken cancellationToken)
    {
      var found = await fUrlRepository.UpdateAsync(id, dto, cancellationToken);
      if (found)
      {
        await fUrlCacheService.SetUrlAsync(dto.Path, dto.Url, context, cancellationToken);
        if (dto.Url != oldDoc.Url)
          await fUrlCacheService.UnsetUrlAsync(oldDoc.Url, context, cancellationToken);
      }

      return found;
    }

    public async Task<bool> DeleteAsync(ObjectId id, ILoggingContext context, CancellationToken cancellationToken)
    {
      var result = await fUrlRepository.RemoveAsync(id, cancellationToken);
      if (result != null)
        await fUrlCacheService.UnsetUrlAsync(result.Path, context, cancellationToken);

      return result != null;
    }
  }
}
