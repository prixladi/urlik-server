using System.ComponentModel.DataAnnotations;

namespace Shamyr.Urlik.Service.Models.Url
{
  public class PutModel
  {
    [Url]
    [Required]
    public string Url { get; set; } = default!;

    [StringLength(Constants._MaxPathLength, MinimumLength = 5)]
    [Required]
    public string Path { get; set; } = default!;
  }
}
