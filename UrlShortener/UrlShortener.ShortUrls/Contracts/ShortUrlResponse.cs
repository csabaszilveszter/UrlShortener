namespace UrlShortener.ShortUrls.UrlShortener;

public record ShortUrlResponse(
  string Url,
  int Value,
  DateTime LastUpdatedAt,
  DateTime ExpiresAt
);
