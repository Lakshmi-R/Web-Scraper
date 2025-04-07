using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Scraper.Repository.Interfaces;
using Scraper.Repository.Models;

namespace Scraper.Repository;
public class SearchResultsRepo : ISearchResultsRepo
{
    private readonly SearchResultDbContext _dbContext;
    private readonly ILogger<SearchResultsRepo> _logger;

    public SearchResultsRepo(SearchResultDbContext dbContext,ILogger<SearchResultsRepo> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<int>> GetPositionAsync()
    {
        var lastRecord = await _dbContext.SearchResults
                                   .OrderByDescending(x => x.Date) 
                                   .FirstOrDefaultAsync();

        return lastRecord?.Position ?? new List<int>();

    }

    public async Task<KeywordPositionModel> GetKeywordPositionsAsync()
    {
        var lastRecord = await _dbContext.SearchResults
                                         .OrderByDescending(x => x.Date)
                                         .Select(x => new KeywordPositionModel
                                         {
                                             SearchKeyword = x.SearchKeyword,
                                             Position = x.Position
                                         }).FirstOrDefaultAsync();

        return lastRecord;        
    }



    public async Task<List<SearchResultsModel>> GetSearchResultsAsync()
    {
        var results = await _dbContext.SearchResults.OrderByDescending(x => x.Date).ToListAsync();

        return results;
    }

    public async Task InsertSearchResultsAsync(string url, string searchString, List<int> position)
    {
        try
        {
            var searchResult = new SearchResultsModel
            {
                SearchUrl = url,
                SearchKeyword = searchString,
                Position = position,
                Date = DateTime.UtcNow
            };

            _dbContext.SearchResults.Add(searchResult);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Insert failed.");
        }
   
    }
}
