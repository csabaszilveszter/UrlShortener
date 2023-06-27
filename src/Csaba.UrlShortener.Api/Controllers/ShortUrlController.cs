using Microsoft.AspNetCore.Mvc;
using Csaba.UrlShortener.Services;
using Csaba.UrlShortener.Data.Entities;
using Csaba.UrlShortener.Models.ShortUrl;

namespace Csaba.UrlShortener.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ShortUrlController : ControllerBase
{
  private readonly IShortUrlService _shortUrlService;
  public ShortUrlController(IShortUrlService shortUrlService)
  {
    _shortUrlService = shortUrlService;
  }

  [HttpGet]
  public IActionResult GetAllShortUrls()
  {
    return Ok(_shortUrlService.GetAll());
  }

  [HttpGet("/go/{token}")]
  public IActionResult Go(string token)
  {
    var shortUrl = _shortUrlService.GetByToken(token);
    return Redirect(shortUrl.Url);
  }

  [HttpGet("{id}")]
  public IActionResult GetShortUrl(int id)
  {
    return Ok(_shortUrlService.GetById(id));
  }

  [HttpPost]
  public IActionResult PostShortUrl(Request createRequest)
  {
    var shortUrl = _shortUrlService.Create(createRequest);
    return Created($"/{nameof(GetShortUrl)}/{shortUrl.ID}", shortUrl);
  }

  [HttpPut("{id}")]
  public IActionResult PutShortUrl(int id, Request updateRequest)
  {
    var shortUrl = _shortUrlService.Update(id, updateRequest);
    return Ok(shortUrl);
  }

  [HttpDelete]
  public IActionResult DeleteShortUrl(int id)
  {
    _shortUrlService.Delete(id);
    return Ok();
  }
}
