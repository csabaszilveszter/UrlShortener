using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Csaba.UrlShortener.Data.Entities;

public class RequestData
{
    public int Id { get; set; }
    public string Endpoint { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public User User { get; set; }
    public string UserAgent { get; set; }
    public string OS { get; set; }
    public IPAddress IpAddress { get; set; }
}
