using System.ComponentModel.DataAnnotations;

namespace Shamyr.Urlik.Service.Models.Url
{
  public record PreviewModel
  {
    [Required]
    public string Url { get; set; } = default!;

    [Required]
    public string Path { get; set; } = default!;
  }
}
