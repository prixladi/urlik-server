using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shamyr.Urlik.Service.Models.Url
{
  public record PreviewsModel
  {
    [Required]
    public ICollection<PreviewModel> Previews { get; init; } = default!;

    [Required]
    public int Count { get; init; }
  }
}
