using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shamyr.Urlik.Service.Requests.Url;

namespace Shamyr.Urlik.Service.Handler.Requests.Url
{
  public class PutRequestHandler: IRequestHandler<PutRequest>
  {
    public Task<Unit> Handle(PutRequest request, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
