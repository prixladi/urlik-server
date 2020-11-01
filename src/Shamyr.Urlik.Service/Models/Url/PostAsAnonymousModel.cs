using System.ComponentModel.DataAnnotations;

namespace Shamyr.Urlik.Service.Models.Url
{
  public record PostAsAnonymousModel
  {
    [Url]
    [Required]
    public string Url { get; init; } = default!;
  }
}
