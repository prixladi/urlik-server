using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Cloud.Authority.Client.Services;
using Shamyr.Urlik.Service.Dtos;
using Shamyr.Urlik.Service.Extensions;
using Shamyr.Urlik.Service.Models.Url;
using Shamyr.Urlik.Service.Repositories;
using Shamyr.Urlik.Service.Requests.Url;

namespace Shamyr.Urlik.Service.Handlers.Requests.Url
{
  public class GetManyRequestHandler: IRequestHandler<GetManyRequest, PreviewsModel>
  {
    private readonly IUrlRepository fUrlRepository;
    private readonly IClaimsIdentityService fClaimsIdentityService;

    public GetManyRequestHandler(IUrlRepository urlRepository, IClaimsIdentityService claimsIdentityService)
    {
      fUrlRepository = urlRepository;
      fClaimsIdentityService = claimsIdentityService;
    }

    public async Task<PreviewsModel> Handle(GetManyRequest request, CancellationToken cancellationToken)
    {
      var identity = fClaimsIdentityService.GetCurrentUser<UserIdentity>();
      if (request.Model.UserId != identity.UserId)
        throw new ForbiddenException($"Queried UserID '{request.Model.UserId}' does not match id of current user ({identity.UserId}).");

      var filter = new FilterDto
      (
        request.Model.Skip,
        request.Model.Take,
        request.Model.UserId
      );

      var docs = await fUrlRepository.GetByFilterAsync(filter, cancellationToken);
      return new PreviewsModel
      {
        Count = await fUrlRepository.CountByFilterAsync(filter, cancellationToken),
        Previews = docs.ToPreviewModel().ToArray()
      };
    }
  }
}
