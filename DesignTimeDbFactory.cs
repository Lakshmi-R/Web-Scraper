using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Scraper.Repository;
using Microsoft.Extensions.Configuration;

public class DesignTimeDbFactory : IDesignTimeDbContextFactory<SearchResultDbContext>
{
   
    public SearchResultDbContext CreateDbContext(string[] args)
    {
       
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

       
        var optionsBuilder = new DbContextOptionsBuilder<SearchResultDbContext>();
        var connectionString = configuration.GetConnectionString("DbConnectionString");
        optionsBuilder.UseSqlServer(connectionString);

        return new SearchResultDbContext(optionsBuilder.Options);
    }
}