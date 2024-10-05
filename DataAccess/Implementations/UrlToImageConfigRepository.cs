using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class UrlToImageConfigRepository : Repository<UrlToImageConfig>, IUrlToImageConfigRepository
    {
        private readonly NewsAggregatorDbContext _context;
        public UrlToImageConfigRepository(NewsAggregatorDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<UrlToImageConfig> GetConfigsByRssSourceIdAsync(int rssFeedId)
        {
            try
            {
                var response = _context.UrlToImageConfigs
                    .Where(x => x.RssFeedId == rssFeedId)
                    .FirstOrDefaultAsync();
                if (response == null)
                {
                    throw new Exception("Not found.");
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
