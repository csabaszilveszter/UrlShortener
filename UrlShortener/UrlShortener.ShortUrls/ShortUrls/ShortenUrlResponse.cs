namespace UrlShortener.ShortUrls.UrlShortener;

public record ShortenUrlResponse(
  int id,
  string Url,
  string ShortUrl,
  DateTime LastUpdateDate,
  DateTime ExpireDate
);
