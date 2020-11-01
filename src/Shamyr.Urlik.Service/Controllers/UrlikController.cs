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
    private static readonly string fDefaultUrl = EnvVariable.Get(EnvVariables._DefaultUrl);

    private readonly ISender fSender;

    public UrlikController(ISender sender)
    {
      fSender = sender;
    }


    /// <summary>
    /// Fallback root URL redirectiong to default URL.
    /// </summary>
    /// <response code="302">Redirected</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public IActionResult Get()
    {
      return Redirect(fDefaultUrl);
    }

    /// <summary>
    /// Redirect to URL shortened by provided path or redirect to default URL if not found.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="302">Redirected</response>
    [HttpGet("{path}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public async Task<IActionResult> GetAsync([FromRoute] string path, CancellationToken cancellationToken)
    {
      string? url = await fSender.Send(new GetUrlRequest(path), cancellationToken);
      return Redirect(url ?? fDefaultUrl);
    }
  }
}
