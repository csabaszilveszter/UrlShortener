using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.Models;
using UrlShortener.Api.Data;

namespace UrlShortener.Api.Controllers;


[Route("api/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]
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
    [HttpGet("{value}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ShortUrl>> GetShortUrl(int value)
    {
        if (_context.ShortUrl == null)
        {
            return NotFound();
        }
        var shortUrl = await _context.ShortUrl.FindAsync(value);

        if (shortUrl == null)
        {
            return NotFound();
        }

        return shortUrl;
    }

    // PUT: api/ShortUrl/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{value}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutShortUrl(int value, ShortUrl shortUrl)
    {
        if (value != shortUrl.Value)
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
            if (!ShortUrlExists(value))
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
    public async Task<ActionResult<ShortUrl>> PostShortUrl(ShortUrl shortUrl)
    {
        if (_context.ShortUrl == null)
        {
            return Problem("Entity set 'ApplicationContext.ShortUrl'  is null.");
        }
        _context.ShortUrl.Add(shortUrl);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(PostShortUrl), new { value = shortUrl.Value }, shortUrl);
    }

    // DELETE: api/ShortUrl/5
    [HttpDelete("{value}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteShortUrl(int value)
    {
        if (_context.ShortUrl == null)
        {
            return NotFound();
        }
        var shortUrl = await _context.ShortUrl.FindAsync(value);
        if (shortUrl == null)
        {
            return NotFound();
        }

        _context.ShortUrl.Remove(shortUrl);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ShortUrlExists(int value)
    {
        return (_context.ShortUrl?.Any(e => e.Value == value)).GetValueOrDefault();
    }
}
