using System.ComponentModel.DataAnnotations;
using Shamyr.Urlik.Service.Validation;

namespace Shamyr.Urlik.Service.Models.Url
{
  public record PutModel
  {
    [Url]
    [Required]
    public string Url { get; init; } = default!;

    /// <summary>
    /// Path that will be used as shortcut
    /// Case insensitive
    /// </summary>
    [StringLength(Constants._MaxPathLength, MinimumLength = 5)]
    [UrlikPath]
    [Required]
    public string Path { get; init; } = default!;
  }
}
