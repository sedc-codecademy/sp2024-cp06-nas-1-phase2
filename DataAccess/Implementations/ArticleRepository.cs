using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                    .Where(x => x.RssSourceId == rssSourceId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
