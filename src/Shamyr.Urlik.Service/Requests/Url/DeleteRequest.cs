using MediatR;
using MongoDB.Bson;

namespace Shamyr.Urlik.Service.Requests.Url
{
  public class DeleteRequest: IRequest
  {
    public ObjectId UrlId { get; }

    public DeleteRequest(ObjectId urlId)
    {
      UrlId = urlId;
    }
  }
}
