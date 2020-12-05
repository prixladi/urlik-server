using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Shamyr.Urlik.Service.Models.Url
{
  public record DetailModel
  {
    [Required]
    public ObjectId Id { get; init; }

    [Required]
    public string Url { get; init; } = default!;

    [Required]
    public string Path { get; init; } = default!;

    [Required]
    public int HitCount { get; init; }
  }
}
