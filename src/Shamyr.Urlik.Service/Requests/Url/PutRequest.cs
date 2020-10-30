using System;
using MediatR;
using MongoDB.Bson;
using Shamyr.Urlik.Service.Models.Url;

namespace Shamyr.Urlik.Service.Requests.Url
{
  public class PutRequest: IRequest
  {
    public ObjectId UrlId { get; }
    public PutModel Model { get; }

    public PutRequest(ObjectId urlId, PutModel model)
    {
      UrlId = urlId;
      Model = model ?? throw new ArgumentNullException(nameof(model));
    }
  }
}
