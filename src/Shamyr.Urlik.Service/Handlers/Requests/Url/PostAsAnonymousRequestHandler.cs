using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Urlik.Service.Models.Url;
using Shamyr.Urlik.Service.Requests.Url;

namespace Shamyr.Urlik.Service.Handler.Requests.Url
{
  public class PostAsAnonymousRequestHandler: IRequestHandler<PostAsAnonymousRequest, DetailModel>
  {
    public Task<DetailModel> Handle(PostAsAnonymousRequest request, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
