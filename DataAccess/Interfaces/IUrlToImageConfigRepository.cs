using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IUrlToImageConfigRepository : IRepository<UrlToImageConfig>
    {
        Task<UrlToImageConfig> GetConfigsByRssSourceIdAsync(int rssFeedId);
    }
}
