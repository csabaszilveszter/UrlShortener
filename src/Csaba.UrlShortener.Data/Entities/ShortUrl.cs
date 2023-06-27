using Microsoft.EntityFrameworkCore;

namespace Csaba.UrlShortener.Data.Entities;

public class ShortUrl
{
    public int ID { get; set; }
    public string Url { get; set; }
    public string UrlShort { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    // public User User { get; set; }
}
