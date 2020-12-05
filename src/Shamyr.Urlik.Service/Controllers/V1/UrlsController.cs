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
  [Route("api/v1/urls")]
  public class UrlsController: ControllerBase
  {
    private const string _GetUrlRoute = "GetUrl";

    private readonly ISender fSender;

    public UrlsController(ISender sender)
    {
      fSender = sender;
    }

    /// <summary>
    /// Creates shortcut for URL using provided path.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="201">URL shortcut created</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="409">Path is already occupied</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(DetailModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PostAsync([FromBody] PostModel model, CancellationToken cancellationToken)
    {
      var result = await fSender.Send(new PostRequest(model), cancellationToken);
      return CreatedAtRoute(_GetUrlRoute, new { urlId = result.Id.ToString() }, result);
    }

    /// <summary>
    /// Creates shortcut for URL using random path.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="201">URL shortcut created</response>
    /// <response code="400">Model is not valid</response>
    [HttpPost("anonymous")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DetailModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsAnonymousAsync([FromBody] PostAsAnonymousModel model, CancellationToken cancellationToken)
    {
      var result = await fSender.Send(new PostAsAnonymousRequest(model), cancellationToken);
      return CreatedAtRoute(_GetUrlRoute, new { urlId = result.Id.ToString() }, result);
    }

    /// <summary>
    /// Gets model of URL shortcut.
    /// </summary>
    /// <param name="urlId"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Returns URL shortcut detail model</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="403">User does not own this url shortcut</response>
    /// <response code="404">URL shortcut with id not found</response>
    [HttpGet("{urlId}", Name = _GetUrlRoute)]
    [Authorize]
    [ProducesResponseType(typeof(DetailModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DetailModel> GetAsync([FromRoute] ObjectId urlId, CancellationToken cancellationToken)
    {
      return await fSender.Send(new GetRequest(urlId), cancellationToken);
    }

    /// <summary>
    /// Gets previews of URL shortcuts using provided filter.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Returns URL shortcuts preview models</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="403">User does have access to some of the queried shortcuts</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(PreviewsModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PreviewsModel> GetManyAsync([FromQuery] QueryModel model, CancellationToken cancellationToken)
    {
      return await fSender.Send(new GetManyRequest(model), cancellationToken);
    }

    /// <summary>
    /// Updates shortcut for URL.
    /// </summary>
    /// <param name="urlId"></param>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">URL shortcut created</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="403">User does not own this url shortcut</response>
    /// <response code="404">URL shortcut with id not found</response>
    [HttpPut("{urlId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAsync([FromRoute] ObjectId urlId, [FromBody] PutModel model, CancellationToken cancellationToken)
    {
      await fSender.Send(new PutRequest(urlId, model), cancellationToken);
      return NoContent();
    }

    /// <summary>
    /// Deletes shortcut for URL.
    /// </summary>
    /// <param name="urlId"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">URL shortcut created</response>
    /// <response code="400">Model is not valid</response>
    /// <response code="403">User does not own this url shortcut</response>
    /// <response code="404">URL shortcut with id not found</response>
    [HttpDelete("{urlId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] ObjectId urlId, CancellationToken cancellationToken)
    {
      await fSender.Send(new DeleteRequest(urlId), cancellationToken);
      return NoContent();
    }
  }
}
