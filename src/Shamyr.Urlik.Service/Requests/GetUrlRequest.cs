using MediatR;

namespace Shamyr.Urlik.Service.Requests
{
  public class GetUrlRequest: IRequest<string?>
  {
    public string Path { get; }

    public GetUrlRequest(string path)
    {
      Path = path;
    }
  }
}
