using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.AspNetCore.ApplicationInsights.Services;
using Shamyr.Cloud.Authority.Client.Services;
using Shamyr.Urlik.Service.Repositories;
using Shamyr.Urlik.Service.Requests.Url;
using Shamyr.Urlik.Service.Services;

namespace Shamyr.Urlik.Service.Handlers.Requests.Url
{
  public class DeleteRequestHandler: IRequestHandler<DeleteRequest>
  {
    private readonly IUrlRepository fUrlRepository;
    private readonly ITelemetryService fTelemetryService;
    private readonly IUrlService fUrlService;
    private readonly IClaimsIdentityService fClaimsIdentityService;

    public DeleteRequestHandler(
      IUrlRepository urlRepository,
      ITelemetryService telemetryService,
      IUrlService urlService,
      IClaimsIdentityService claimsIdentityService)
    {
      fUrlRepository = urlRepository;
      fTelemetryService = telemetryService;
      fUrlService = urlService;
      fClaimsIdentityService = claimsIdentityService;
    }

    public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken)
    {
      var doc = await fUrlRepository.GetAsync(request.UrlId, cancellationToken);
      if (doc == null)
        throw new NotFoundException($"Url shortcut with ID '{request.UrlId}' not found.");

      var identity = fClaimsIdentityService.GetCurrentUser<UserIdentity>();
      if (doc.UserId != identity.UserId)
        throw new ForbiddenException($"Current user is unable to access resource with id {request.UrlId}");

      var context = fTelemetryService.GetRequestContext();

      await fUrlService.DeleteAsync(request.UrlId, context, cancellationToken);
      return Unit.Value;
    }
  }
}
