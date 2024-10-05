using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

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

        public async Task AddRangeAsync(IEnumerable<Article> articles)
        {
            try
            {
                foreach (var article in articles)
                {
                    await _context.Articles.AddAsync(article);
                    await _context.SaveChangesAsync();
                }
                //await _context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                //{
                //    await _context.Articles.AddRangeAsync(articles);
                //    await _context.SaveChangesAsync();
                //});
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Article> GetLatestArticleByRssFeedIdAsync(int rssFeedId)
        {
            return await _context.Articles
                .Where(x => x.RssFeedId == rssFeedId)
                .OrderByDescending(x => x.PubDate)
                .FirstOrDefaultAsync();
        }
    }
}
