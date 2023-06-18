using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }
  [Required]
  public string Url { get; private set; }
  public int Value { get; private set; }
  public DateTime LastUpdatedAt { get; private set; }
  public DateTime ExpiresAt { get; private set; }
}
