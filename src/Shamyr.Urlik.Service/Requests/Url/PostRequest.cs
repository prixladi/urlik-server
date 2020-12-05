using System;
using MediatR;
using Shamyr.Urlik.Service.Models.Url;

namespace Shamyr.Urlik.Service.Requests.Url
{
  public class PostRequest: IRequest<DetailModel>
  {
    public PostModel Model { get; }

    public PostRequest(PostModel model)
    {
      Model = model ?? throw new ArgumentNullException(nameof(model));
    }
  }
}
