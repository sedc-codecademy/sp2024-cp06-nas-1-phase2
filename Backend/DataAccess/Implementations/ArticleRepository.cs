using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using Shared.Modules;

namespace DataAccess.Implementations
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly NewsAggregatorDbContext _context;
        public ArticleRepository(NewsAggregatorDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetArticlesByRssSourceIdAsync(int rssSourceId)
        {
            try
            {
                return await _context.Articles
                    .Where(x => x.RssFeedId == rssSourceId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Article>> GetArticlesByTitleAndLinkAsync(IEnumerable<string> titles,
            IEnumerable<string> links)
        {
            return await _context.Articles
                .Where(x => titles.Contains(x.Title) && links.Contains(x.Link))
                .ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Article> articles, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Starting AddRangeAsync method");

                await _context.Articles.AddRangeAsync(articles, cancellationToken);
                Console.WriteLine("Articles added");

                await _context.SaveChangesAsync(cancellationToken);
                Console.WriteLine("Changes saved");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<Article> GetLatestArticleByRssFeedIdAsync(int rssFeedId)
        {
            return (await _context.Articles
                .Where(x => x.RssFeedId == rssFeedId)
                .OrderByDescending(x => x.PubDate)
                .FirstOrDefaultAsync())!;
        }

        public async Task<IEnumerable<Article>> GetPagedArticlesByRssSourceIdAsync(int rssSourceId, int pageNumber, int pageSize)
        {
            try
            {
                return await _context.Articles
                    .Where(x => x.RssFeedId == rssSourceId)
                    .Skip((pageNumber - 1) * pageSize) // Skips records for previous pages
                    .Take(pageSize) // Takes the specified number of records
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Article>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            try
            {
                return await _context.Articles
                   .OrderByDescending(article => article.PubDate) // Order by the actual DateTime property
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .ToListAsync(); // Execute the query and retrieve the data
                 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Articles.CountAsync();
        }
    }
}
