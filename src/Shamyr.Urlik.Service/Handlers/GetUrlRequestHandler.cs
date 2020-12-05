using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.AspNetCore.ApplicationInsights.Services;
using Shamyr.Logging;
using Shamyr.Urlik.Service.Dtos;
using Shamyr.Urlik.Service.Requests;
using Shamyr.Urlik.Service.Services;
using StackExchange.Redis;

namespace Shamyr.Urlik.Service.Handlers
{
  public class GetUrlRequestHandler: IRequestHandler<GetUrlRequest, string?>
  {
    private readonly ILogger fLogger;
    private readonly ITelemetryService fTelemetryService;
    private readonly IUrlService fUrlService;
    private readonly IRedisService fRedisService;

    public GetUrlRequestHandler(
      ILogger logger,
      ITelemetryService telemetryService,
      IUrlService urlService,
      IRedisService redisService)
    {
      fLogger = logger;
      fTelemetryService = telemetryService;
      fUrlService = urlService;
      fRedisService = redisService;
    }

    public async Task<string?> Handle(GetUrlRequest request, CancellationToken cancellationToken)
    {
      var context = fTelemetryService.GetRequestContext();
      string? url = null;
      try
      {
        url = await fUrlService.TryGetAsync(request.Path, context, cancellationToken);
        if (url != null)
        {
          await fRedisService.PushHitAsync(new HitDto
          {
            HitUtc = DateTime.UtcNow,
            Path = request.Path
          }, CommandFlags.FireAndForget, cancellationToken);
        }
      }
      catch (Exception ex)
      {
        fLogger.LogException(context, ex);
      }

      return url;
    }
  }
}
