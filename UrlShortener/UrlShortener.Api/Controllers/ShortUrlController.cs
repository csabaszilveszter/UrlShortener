using Microsoft.AspNetCore.Mvc;
using UrlShortener.ShortUrls.UrlShortener;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShortUrlController : ControllerBase
{
  [HttpPost()]
  public IActionResult ShortUrl(ShortenUrlRequest request)
  {
    return Ok(
        new ShortenUrlResponse(
            0,
            request.Url,
            $"{Request.Scheme}://{Request.Host}/0",  // TODO: proper shorten
            DateTime.Now,
            DateTime.Now.AddDays(1)
        )
    );
  }

  [HttpGet("{id:int}")]
  public IActionResult GetShortUrl(int id)
  {
    return Redirect(
      $"{Request.Scheme}://{Request.Host}/{id}"
    );
  }

  [HttpDelete("{id:int}")]
  public IActionResult DeleteShortUrl(int id)
  {
    return NoContent();
  }
}
