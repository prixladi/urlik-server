using MediatR;
using Shamyr.Urlik.Service.Models.Url;

namespace Shamyr.Urlik.Service.Requests.Url
{
  public class GetManyRequest: IRequest<PreviewsModel>
  {
    public QueryModel Model { get; }

    public GetManyRequest(QueryModel model)
    {
      Model = model ?? throw new System.ArgumentNullException(nameof(model));
    }
  }
}
