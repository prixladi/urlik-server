using MediatR;
using MongoDB.Bson;
using Shamyr.Urlik.Service.Models.Url;

namespace Shamyr.Urlik.Service.Requests.Url
{
  public class GetRequest: IRequest<DetailModel>
  {
    public ObjectId UrlId { get; }

    public GetRequest(ObjectId urlId)
    {
      UrlId = urlId;
    }
  }
}
