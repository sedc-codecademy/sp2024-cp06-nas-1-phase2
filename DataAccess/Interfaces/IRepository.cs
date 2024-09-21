using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IRepository<T> where T : BaseClass
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        //List<RssSource> GetBySource(string source);
        int Add(T entity);
        int Remove(int id);
    }
}
