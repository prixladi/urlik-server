using System;
using MediatR;
using Shamyr.Urlik.Service.Models.Url;

namespace Shamyr.Urlik.Service.Requests.Url
{
  public class PostAsAnonymousRequest: IRequest<DetailModel>
  {
    public PostAsAnonymousModel Model { get; }

    public PostAsAnonymousRequest(PostAsAnonymousModel model)
    {
      Model = model ?? throw new ArgumentNullException(nameof(model));
    }
  }
}
