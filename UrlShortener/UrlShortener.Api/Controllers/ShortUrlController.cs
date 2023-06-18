using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.Data;
using UrlShortener.Api.Models;
using UrlShortener.ShortUrls.UrlShortener;

namespace UrlShortener.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShortUrlController : ControllerBase
{
    private readonly ApplicationContext _context;

    public ShortUrlController(ApplicationContext context)
    {
        _context = context;
    }

    // GET: api/ShortUrl
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<ShortUrl>>> GetShortUrl()
    {
        if (_context.ShortUrl == null)
        {
            return NotFound();
        }
        return await _context.ShortUrl.ToListAsync();
    }

    // GET: api/ShortUrl/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ShortUrl>> GetShortUrl(Guid id)
    {
        if (_context.ShortUrl == null)
        {
            return NotFound();
        }
        var shortUrl = await _context.ShortUrl.FindAsync(id);

        if (shortUrl == null)
        {
            return NotFound();
        }

        return shortUrl;
    }

    // PUT: api/ShortUrl/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutShortUrl(Guid id, ShortUrl shortUrl)
    {
        if (id != shortUrl.Id)
        {
            return BadRequest();
        }

        _context.Entry(shortUrl).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShortUrlExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/ShortUrl
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ShortUrl>> PostShortUrl(ShortUrlRequest request)
    {
        if (_context.ShortUrl == null)
        {
            return Problem("Entity set 'ApplicationContext.ShortUrl'  is null.");
        }
        if (!Uri.IsWellFormedUriString(request.Url, UriKind.Absolute)) {
            return BadRequest($"'{request.Url}' is not a valid URL.");
        }
        var shortUrl = new ShortUrl(request.Url);
        _context.ShortUrl.Add(shortUrl);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetShortUrl), new { id = shortUrl.Id }, shortUrl);
    }

    // DELETE: api/ShortUrl/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteShortUrl(Guid id)
    {
        if (_context.ShortUrl == null)
        {
            return NotFound();
        }
        var shortUrl = await _context.ShortUrl.FindAsync(id);
        if (shortUrl == null)
        {
            return NotFound();
        }

        _context.ShortUrl.Remove(shortUrl);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ShortUrlExists(Guid id)
    {
        return (_context.ShortUrl?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}

