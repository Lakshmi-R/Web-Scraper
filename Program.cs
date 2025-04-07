using Microsoft.EntityFrameworkCore;
using Scraper.Repository;
using Scraper.Repository.Interfaces;
using Scraper.Core.Interfaces;
using Scraper.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();

builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SearchResultDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"));
});

builder.Services.AddScoped<ISearchResultsRepo, SearchResultsRepo>();
builder.Services.AddScoped<IScraperService, ScraperService>();

builder.Services.AddCorsPolicy(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
    app.UseSwagger();
    
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// cors middleware
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
