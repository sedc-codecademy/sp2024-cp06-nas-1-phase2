using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<IEnumerable<Article>> GetArticlesByRssSourceIdAsync(int rssSourceId);
        Task<IEnumerable<Article>> 
            GetArticlesByTitleAndLinkAsync(IEnumerable<string> titles, IEnumerable<string> links);
        Task<Article> GetLatestArticleByRssFeedIdAsync(int rssFeedId);
        Task<IEnumerable<Article>> GetPagedArticlesByRssSourceIdAsync(int rssSourceId, int pageNumber, int pageSize);
        Task<IEnumerable<Article>> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
        
        Task AddRangeAsync(IEnumerable<Article> articles, CancellationToken cancellationToken);
    }
}
