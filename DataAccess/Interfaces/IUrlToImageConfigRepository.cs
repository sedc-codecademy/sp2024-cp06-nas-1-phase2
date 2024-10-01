using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IUrlToImageConfigRepository : IRepository<UrlToImageConfig>
    {
        Task<IEnumerable<UrlToImageConfig>> GetConfigsByRssSourceIdAsync(int rssSourceId);
    }
}
