using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IRssFeedRepository : IRepository<RssFeed>
    {
        Task<RssFeed> GetBySourceAsync(string source);
    }
}
