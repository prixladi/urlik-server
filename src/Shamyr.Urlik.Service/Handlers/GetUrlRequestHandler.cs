using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shamyr.AspNetCore.Retrievers;
using Shamyr.Logging;
using Shamyr.Urlik.Service.Requests;
using Shamyr.Urlik.Service.Services;

namespace Shamyr.Urlik.Service.Handlers
{
  public class GetUrlRequestHandler: IRequestHandler<GetUrlRequest, string>
  {
    private readonly ILogger fLogger;
    private readonly ILoggingContextRetriever fLoggingContextRetriever;
    private readonly IHttpContextAccessor fHttpContextAccessor;
    private readonly IUrlService fUrlService;

    public GetUrlRequestHandler(
      ILogger logger,
      ILoggingContextRetriever loggingContextRetriever,
      IHttpContextAccessor httpContextAccessor,
      IUrlService urlService)
    {
      fLogger = logger;
      fLoggingContextRetriever = loggingContextRetriever;
      fHttpContextAccessor = httpContextAccessor;
      fUrlService = urlService;
    }

    public async Task<string> Handle(GetUrlRequest request, CancellationToken cancellationToken)
    {
      string? url = null;
      try
      {
        url = await fUrlService.TryGetAsync(request.Path, cancellationToken);
      }
      catch (Exception ex)
      {
        var httpContext = fHttpContextAccessor.HttpContext ?? throw new InvalidOperationException("Unable to access http context.");
        var context = fLoggingContextRetriever.Retrieve(httpContext);
        fLogger.LogException(context, ex);
      }

      return url ?? EnvVariable.Get(EnvVariables._DefaultUrl);
    }
  }
}
