using System.Security.Claims;
using MongoDB.Bson;

namespace Shamyr.Urlik.Service
{
  public class UserIdentity: ClaimsIdentity
  {
    public ObjectId UserId { get; }

    public UserIdentity(ObjectId userId, string authType)
      : base(authType)
    {
      UserId = userId;
    }
  }
}
