using System.Runtime.CompilerServices;
using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataAccess.Implementations
{
    public class RssFeedRepository : Repository<RssFeed>, IRssFeedRepository
    {
        private readonly NewsAggregatorDbContext _context;
        public RssFeedRepository(NewsAggregatorDbContext context) : base(context)
        {
            _context = context;
        }

        //public List<RssFeed> GetBySource(string source)
        //{
        //    var sources = _context.RssSources.Where(x => x.Source.Contains(source));
        //    return sources.ToList();
        //}

        public async Task<RssFeed> GetBySourceAsync(string source)
        {
            try
            {
                return await _context.RssFeeds.FirstOrDefaultAsync(rs => rs.FeedUrl.Contains(source));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
