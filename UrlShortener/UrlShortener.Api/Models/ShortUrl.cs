using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Api.Models;

public class ShortUrl
{
  public ShortUrl(string url)
  {
    Url = url;
    Value = url.GetHashCode();
    LastUpdatedAt = DateTime.UtcNow;
    ExpiresAt = LastUpdatedAt.AddMinutes(1);
  }
  public string Url { get; private set; }
  [Key]
  public int Value { get; private set; }
  public DateTime LastUpdatedAt { get; private set; }
  public DateTime ExpiresAt { get; private set; }
}
