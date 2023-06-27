using System.ComponentModel.DataAnnotations;

namespace Csaba.UrlShortener.Models.ShortUrl;

public class Request
{
  [Required]
  public string Url { get; set; }
}
