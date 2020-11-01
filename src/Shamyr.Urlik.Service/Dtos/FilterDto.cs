using MongoDB.Bson;

namespace Shamyr.Urlik.Service.Dtos
{
  public record FilterDto(
    int Skip,
    int Take,
    ObjectId UserId);
}
