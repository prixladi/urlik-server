using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Shamyr.Urlik.Service.Models.Url
{
  public record DetailModel
  {
    public ObjectId Id { get; set; }

    [Required]
    public string Url { get; set; } = default!;
    
    [Required]
    public string Path { get; set; } = default!;
  }
}
