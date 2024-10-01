using System.Runtime.CompilerServices;
using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataAccess.Implementations
{
    public class RssSourceRepository : Repository<RssSource>, IRssSourceRepository
    {
        private readonly NewsAggregatorDbContext _context;
        public RssSourceRepository(NewsAggregatorDbContext context) : base(context)
        {
            _context = context;
        }

        //public List<RssSource> GetBySource(string source)
        //{
        //    var sources = _context.RssSources.Where(x => x.Source.Contains(source));
        //    return sources.ToList();
        //}

        public async Task<RssSource> GetBySourceAsync(string source)
        {
            try
            {
                return await _context.RssSources.FirstOrDefaultAsync(rs => rs.FeedUrl.Contains(source));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
