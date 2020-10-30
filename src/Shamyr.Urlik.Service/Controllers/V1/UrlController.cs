using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Shamyr.Urlik.Service.Models.Url;
using Shamyr.Urlik.Service.Requests.Url;

namespace Shamyr.Urlik.Service.Controllers
{
  [ApiController]
  [Route("api/v1/url")]
  public class UrlController: ControllerBase
  {
    private const string _GetUrlRoute = "GetUrl";

    private readonly ISender fSender;

    public UrlController(ISender sender)
    {
      fSender = sender;
    }

    /// <summary>
    /// Creates shortcut for url using provided path.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="201">Url shortcut created</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="409">Path is already occupied</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PostAsync([FromBody] PostModel model, CancellationToken cancellationToken)
    {
      var result = await fSender.Send(new PostRequest(model), cancellationToken);
      return CreatedAtRoute(_GetUrlRoute, new { id = result.Id.ToString() }, result);
    }

    /// <summary>
    /// Creates shortcut for url using random path.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="201">Url shortcut created</response>
    /// <response code="400">Model is not valid</response>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsAnonymousAsync([FromBody] PostAsAnonymousModel model, CancellationToken cancellationToken)
    {
      var result = await fSender.Send(new PostAsAnonymousRequest(model), cancellationToken);
      return CreatedAtRoute(_GetUrlRoute, new { id = result.Id.ToString() }, result);
    }

    /// <summary>
    /// Updates shortcut for url.
    /// </summary>
    /// <param name="urlId"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Url shortcut created</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="404">Url shortcut with id not found</response>
    [HttpPut("{urlId}")]
    [ProducesResponseType(typeof(DetailModel), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAsync([FromRoute]ObjectId urlId, [FromBody] PutModel model, CancellationToken cancellationToken)
    {
      await fSender.Send(new PutRequest(urlId, model), cancellationToken);
      return NoContent();
    }

    /// <summary>
    /// Gets model of url shortcut.
    /// </summary>
    /// <param name="urlId"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Url shortcut created</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="404">Url shortcut with id not found</response>
    [HttpGet("{urlId}", Name = _GetUrlRoute)]
    [ProducesResponseType(typeof(DetailModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DetailModel> GetAsync([FromRoute] ObjectId urlId, CancellationToken cancellationToken)
    {
      return await fSender.Send(new GetRequest(urlId), cancellationToken);
    }
  }
}
