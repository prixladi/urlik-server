using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.AspNetCore.ApplicationInsights.Services;
using Shamyr.Logging;
using Shamyr.Urlik.Service.Requests;
using Shamyr.Urlik.Service.Services;

namespace Shamyr.Urlik.Service.Handlers
{
  public class GetUrlRequestHandler: IRequestHandler<GetUrlRequest, string?>
  {
    private readonly ILogger fLogger;
    private readonly ITelemetryService fTelemetryService;
    private readonly IUrlService fUrlService;

    public GetUrlRequestHandler(ILogger logger, ITelemetryService telemetryService, IUrlService urlService)
    {
      fLogger = logger;
      fTelemetryService = telemetryService;
      fUrlService = urlService;
    }

    public async Task<string?> Handle(GetUrlRequest request, CancellationToken cancellationToken)
    {
      var context = fTelemetryService.GetRequestContext();
      string? url = null;

      try
      {
        url = await fUrlService.TryGetAsync(request.Path, context, cancellationToken);
      }
      catch (Exception ex)
      {
        fLogger.LogException(context, ex);
      }

      return url;
    }
  }
}
