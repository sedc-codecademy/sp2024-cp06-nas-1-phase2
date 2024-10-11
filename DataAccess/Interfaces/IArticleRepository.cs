using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<IEnumerable<Article>> GetArticlesByRssSourceIdAsync(int rssSourceId);
        Task AddRangeAsync(IEnumerable<Article> articles, CancellationToken cancellationToken);
        Task<IEnumerable<Article>>
            GetArticlesByTitleAndLinkAsync(IEnumerable<string> titles, IEnumerable<string> links);
        Task<Article> GetLatestArticleByRssFeedIdAsync(int rssFeedId);
    }
}
