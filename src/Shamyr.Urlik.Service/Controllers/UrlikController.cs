using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shamyr.Urlik.Service.Requests;

namespace Shamyr.Urlik.Service.Controllers
{
  [ApiController]
  [Route("/")]
  public class UrlikController: ControllerBase
  {
    private readonly ISender fSender;

    public UrlikController(ISender sender)
    {
      fSender = sender;
    }

    /// <summary>
    /// Redirect to url shortened by provided path or redirect to default url if not found.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="302">Redirect</response>
    [HttpGet("{path}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public async Task<IActionResult> GetAsync([FromRoute] string path, CancellationToken cancellationToken)
    {
      string url = await fSender.Send(new GetUrlRequest(path), cancellationToken);
      return Redirect(url);
    }
  }
}
