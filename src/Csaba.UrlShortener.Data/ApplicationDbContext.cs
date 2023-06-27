using Microsoft.EntityFrameworkCore;
using Csaba.UrlShortener.Data.Entities;

namespace Csaba.UrlShortener.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }
    public DbSet<ShortUrl> ShortUrls { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RequestData> RequestDatas{ get; set; }
}
