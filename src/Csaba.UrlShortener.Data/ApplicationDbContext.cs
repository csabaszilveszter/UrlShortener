using Microsoft.EntityFrameworkCore;
using Csaba.UrlShortener.Entities;

namespace Csaba.UrlShortener.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }

    public DbSet<ShortUrl> ShortUrls { get; set; }

    public DbSet<User> Users { get; set; }
}
