using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.AspNetCore.ApplicationInsights.Services;
using Shamyr.Urlik.Database.Documents;
using Shamyr.Urlik.Service.Extensions;
using Shamyr.Urlik.Service.Models.Url;
using Shamyr.Urlik.Service.Requests.Url;
using Shamyr.Urlik.Service.Services;

namespace Shamyr.Urlik.Service.Handler.Requests.Url
{
  public class PostAsAnonymousRequestHandler: IRequestHandler<PostAsAnonymousRequest, DetailModel>
  {
    private readonly ITelemetryService fTelemetryService;
    private readonly IUrlService fUrlService;
    private readonly IRandomService fRandomService;

    public PostAsAnonymousRequestHandler(
      ITelemetryService telemetryService,
      IUrlService urlService,
      IRandomService randomService)
    {
      fTelemetryService = telemetryService;
      fUrlService = urlService;
      fRandomService = randomService;
    }

    public async Task<DetailModel> Handle(PostAsAnonymousRequest request, CancellationToken cancellationToken)
    {
      var context = fTelemetryService.GetRequestContext();

      var path = fRandomService.GeneratePath(Constants._MinPathLength);
      var newDoc = new UrlDoc
      {
        Path = path,
        Url = request.Model.Url
      };

      await fUrlService.CreateAsync(newDoc, context, cancellationToken);
      return newDoc.ToDetailModel();
    }
  }
}
