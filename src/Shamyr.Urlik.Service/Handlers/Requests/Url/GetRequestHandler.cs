using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Urlik.Service.Models.Url;
using Shamyr.Urlik.Service.Requests.Url;

namespace Shamyr.Urlik.Service.Handler.Requests.Url
{
  public class GetRequestHandler: IRequestHandler<GetRequest, DetailModel>
  {
    public Task<DetailModel> Handle(GetRequest request, CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }
  }
}
