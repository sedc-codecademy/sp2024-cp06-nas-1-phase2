using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseClass
    {
        private readonly NewsAggregatorDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(NewsAggregatorDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }
        
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _table.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong.", ex.InnerException);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                if (id < 0 || id > _table.Count())
                {
                    throw new KeyNotFoundException($"Entity with id: {id} is not found.");
                }
                return (await _table.FindAsync(id))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task AddAsync(T entity)
        {
            try
            {
                await _table.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _table.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await _table.FindAsync(id);
                if (entity != null)
                {
                    _table.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Entity with id: {id} is not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
