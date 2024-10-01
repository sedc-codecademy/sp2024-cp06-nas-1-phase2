using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IRssSourceRepository : IRepository<RssSource>
    {
        Task<RssSource> GetBySourceAsync(string source);
    }
}
