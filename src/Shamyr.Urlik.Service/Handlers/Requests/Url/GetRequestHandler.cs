using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Urlik.Service.Extensions;
using Shamyr.Urlik.Service.Models.Url;
using Shamyr.Urlik.Service.Repositories;
using Shamyr.Urlik.Service.Requests.Url;

namespace Shamyr.Urlik.Service.Handler.Requests.Url
{
  public class GetRequestHandler: IRequestHandler<GetRequest, DetailModel>
  {
    private readonly IUrlRepository fUrlRepository;

    public GetRequestHandler(IUrlRepository urlRepository)
    {
      fUrlRepository = urlRepository;
    }

    public async Task<DetailModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
      var doc = await fUrlRepository.GetAsync(request.UrlId, cancellationToken);
      if (doc == null)
        throw new NotFoundException($"Url shortcut with ID '{request.UrlId}' not found.");

      return doc.ToDetailModel();
    }
  }
}
