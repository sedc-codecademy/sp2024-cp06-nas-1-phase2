using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class RssFeedRepository : Repository<RssFeed>, IRssFeedRepository
    {
        private readonly NewsAggregatorDbContext _context;
        public RssFeedRepository(NewsAggregatorDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<RssFeed> GetBySourceAsync(string source)
        {
            try
            {
                return (await _context.RssFeeds.FirstOrDefaultAsync(rs => rs.FeedUrl.Contains(source)))!;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Database update error: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
