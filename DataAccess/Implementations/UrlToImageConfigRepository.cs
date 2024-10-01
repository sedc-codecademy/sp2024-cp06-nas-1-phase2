using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataAccess.Implementations
{
    public class UrlToImageConfigRepository : Repository<UrlToImageConfig>, IUrlToImageConfigRepository
    {
        private readonly NewsAggregatorDbContext _context;
        public UrlToImageConfigRepository(NewsAggregatorDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UrlToImageConfig>> GetConfigsByRssSourceIdAsync(int rssSourceId)
        {
            try
            {
                return await _context.UrlToImageConfigs
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
