using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IRepository<T> where T : BaseClass
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
