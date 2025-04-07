using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Scraper.Repository.Models;

// file scoped namespace
namespace Scraper.Repository;
public class SearchResultDbContext(DbContextOptions options) : DbContext(options)
{
    private readonly IConfiguration _configuration;

    public DbSet<SearchResultsModel> SearchResults { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SearchResultsModel>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.SearchKeyword).IsRequired().HasMaxLength(100);
            entity.Property(x => x.SearchUrl).IsRequired().HasMaxLength(150);
            entity.Property(x => x.Position);
            entity.Property(x => x.Date).IsRequired();
        });
    }
}
