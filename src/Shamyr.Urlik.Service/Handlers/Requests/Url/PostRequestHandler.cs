using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.AspNetCore.ApplicationInsights.Services;
using Shamyr.Cloud.Authority.Client.Services;
using Shamyr.Urlik.Database.Documents;
using Shamyr.Urlik.Service.Extensions;
using Shamyr.Urlik.Service.Models.Url;
using Shamyr.Urlik.Service.Repositories;
using Shamyr.Urlik.Service.Requests.Url;
using Shamyr.Urlik.Service.Services;

namespace Shamyr.Urlik.Service.Handler.Requests.Url
{
  public class PostRequestHandler: IRequestHandler<PostRequest, DetailModel>
  {
    private readonly ITelemetryService fTelemetryService;
    private readonly IUrlService fUrlService;
    private readonly IUrlRepository fUrlRepository;
    private readonly IClaimsIdentityService fClaimsIdentityService;

    public PostRequestHandler(
      ITelemetryService telemetryService,
      IUrlService urlService,
      IUrlRepository urlRepository,
      IClaimsIdentityService claimsIdentityService)
    {
      fTelemetryService = telemetryService;
      fUrlService = urlService;
      fUrlRepository = urlRepository;
      fClaimsIdentityService = claimsIdentityService;
    }

    public async Task<DetailModel> Handle(PostRequest request, CancellationToken cancellationToken)
    {
      if (await fUrlRepository.ExistByPathAsync(request.Model.Path, cancellationToken))
        throw new ConflictException($"Path '{request.Model.Path}' is already occuepied.");

      var context = fTelemetryService.GetRequestContext();

      var identity = fClaimsIdentityService.GetCurrentUser<UserIdentity>();

      var newDoc = new UrlDoc
      {
        Path = request.Model.Path,
        Url = request.Model.Url,
        UserId = identity.UserId
      };

      await fUrlService.CreateAsync(newDoc, context, cancellationToken);
      return newDoc.ToDetailModel();
    }
  }
}
