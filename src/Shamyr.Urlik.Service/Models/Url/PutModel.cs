using System.ComponentModel.DataAnnotations;

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
    [Required]
    public string Path { get; init; } = default!;
  }
}
