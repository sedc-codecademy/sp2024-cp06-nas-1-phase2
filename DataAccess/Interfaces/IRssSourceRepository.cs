using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IRssSourceRepository : IRepository<RssSource>
    {
        public List<RssSource> GetBySource(string source);
    }
}
