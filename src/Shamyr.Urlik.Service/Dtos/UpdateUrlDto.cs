namespace Shamyr.Urlik.Service.Dtos
{
  public record UpdateUrlDto
  {
    public string Url { get; init; } = default!;
    public string Path { get; init; } = default!;
  }
}
