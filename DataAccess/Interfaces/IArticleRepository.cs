using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<IEnumerable<Article>> GetArticlesByRssSourceIdAsync(int rssSourceId);
    }
}
