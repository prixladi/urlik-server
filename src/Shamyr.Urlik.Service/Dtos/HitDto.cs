using System;

namespace Shamyr.Urlik.Service.Dtos
{
  public record HitDto
  {
    public DateTime HitUtc { get; init; }
    public string Path { get; init; } = default!;
  }
}
