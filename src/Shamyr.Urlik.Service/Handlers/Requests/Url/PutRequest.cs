using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.AspNetCore.ApplicationInsights.Services;
using Shamyr.Cloud.Authority.Client.Services;
using Shamyr.Urlik.Service.Dtos;
using Shamyr.Urlik.Service.Repositories;
using Shamyr.Urlik.Service.Requests.Url;
using Shamyr.Urlik.Service.Services;

namespace Shamyr.Urlik.Service.Handler.Requests.Url
{
  public class PutRequestHandler: IRequestHandler<PutRequest>
  {
    private readonly ITelemetryService fTelemetryService;
    private readonly IUrlService fUrlService;
    private readonly IUrlRepository fUrlRepository;
    private readonly IClaimsIdentityService fClaimsIdentityService;

    public PutRequestHandler(
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

    public async Task<Unit> Handle(PutRequest request, CancellationToken cancellationToken)
    {
      var doc = await fUrlRepository.GetAsync(request.UrlId, cancellationToken);
      if (doc == null)
        throw new NotFoundException($"Url shortcut with ID '{request.UrlId}' not found.");

      var identity = fClaimsIdentityService.GetCurrentUser<UserIdentity>();
      if (doc.UserId != identity.UserId)
        throw new ForbiddenException($"Current user is unable to access resource with id {request.UrlId}");

      if (doc.Url != request.Model.Url && await fUrlRepository.ExistByPathAsync(request.Model.Path, cancellationToken))
        throw new ConflictException($"Path '{request.Model.Path}' is already occuepied.");

      var context = fTelemetryService.GetRequestContext();

      var updateDto = new UpdateUrlDto
      {
        Path = request.Model.Path,
        Url = request.Model.Url
      };
      await fUrlService.UpdateAsync(request.UrlId, updateDto, doc, context, cancellationToken);

      return Unit.Value;
    }
  }
}
