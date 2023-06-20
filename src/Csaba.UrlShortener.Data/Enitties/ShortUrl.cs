namespace Csaba.UrlShortener.Entities;

public class ShortUrl
{
    public int ID { get; set; }
    public string Url { get; set; }
    public string UrlShort { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public User User { get; set; }
}
