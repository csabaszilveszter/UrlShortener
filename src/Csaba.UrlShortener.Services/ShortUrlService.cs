using System;
using System.Security.Cryptography;
using System.Text;
using Csaba.UrlShortener.Models.ShortUrl;
using Csaba.UrlShortener.Data;
using Csaba.UrlShortener.Data.Entities;

namespace Csaba.UrlShortener.Services;

public interface IShortUrlService
{
  IEnumerable<ShortUrl> GetAll();
  ShortUrl GetById(int id);
  ShortUrl GetByToken(string token);
  ShortUrl Create(Request createRequest);
  ShortUrl Update(int id, Request updateRequest);
  void Delete(int id);
}

public class ShortUrlService : IShortUrlService
{
  private readonly ApplicationDbContext _dbContext;

  public ShortUrlService(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  static string GenerateHash(string inputString)
  {
    SHA256 sha256Hash = SHA256.Create();
    byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    var stringBuilder = new StringBuilder();
    for (int i = 0; i < data.Length; i++)
    {
      stringBuilder.Append(data[i].ToString("x2"));
    }
    return stringBuilder.ToString();
  }

  public IEnumerable<ShortUrl> GetAll() => _dbContext.ShortUrls;

  public ShortUrl GetByToken(string token) => _dbContext.ShortUrls.Where(s => s.UrlShort == token).FirstOrDefault() ?? throw new KeyNotFoundException($"ShortUrl({token}) not found");

  public ShortUrl GetById(int id) => _dbContext.ShortUrls.Find(id) ?? throw new KeyNotFoundException($"ShortUrl({id}) not found");

  public ShortUrl Create(Request createRequest)
  {
    var shortUrl = new ShortUrl
    {
      Url = createRequest.Url,
      UrlShort = GenerateHash(createRequest.Url),
      UpdatedAt = DateTimeOffset.Now,
      ExpiresAt = DateTimeOffset.Now.AddMinutes(1)
    };
    _dbContext.ShortUrls.Add(shortUrl);
    _dbContext.SaveChanges();
    return shortUrl;
  }


  public ShortUrl Update(int id, Request updateRequest)
  {
    var shortUrl = _dbContext.ShortUrls.Find(id) ?? throw new KeyNotFoundException($"ShortUrl({id}) not found");
    shortUrl.Url = updateRequest.Url;
    shortUrl.UrlShort = GenerateHash(updateRequest.Url);
    shortUrl.UpdatedAt = DateTimeOffset.Now;
    shortUrl.ExpiresAt = shortUrl.UpdatedAt.AddMinutes(1);
    _dbContext.ShortUrls.Update(shortUrl);
    _dbContext.SaveChanges();
    return shortUrl;
  }

  public void Delete(int id)
  {
    var shortUrl = _dbContext.ShortUrls.Find(id) ?? throw new KeyNotFoundException($"ShortUrl({id}) not found");
    _dbContext.ShortUrls.Remove(shortUrl);
    _dbContext.SaveChanges();
  }
}
