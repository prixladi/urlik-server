using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Urlik.Service.Models.Url;
using Shamyr.Urlik.Service.Requests.Url;

namespace Shamyr.Urlik.Service.Handler.Requests.Url
{
  public class PostRequestHandler: IRequestHandler<PostRequest, DetailModel>
  {
    public Task<DetailModel> Handle(PostRequest request, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
